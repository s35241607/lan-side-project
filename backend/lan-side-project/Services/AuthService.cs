using ErrorOr;
using lan_side_project.DTOs.Responses;
using lan_side_project.DTOs.Responses.Auth;
using lan_side_project.DTOs.Requests.Auth;
using lan_side_project.Models;
using lan_side_project.Repositories;
using lan_side_project.Utils;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;

namespace lan_side_project.Services;

public class AuthService(UserRepository userRepository, JwtUtils jwtUtils, MailService mailService, IConfiguration config)
{
    private readonly int RESET_PASSWORD_TOKEN_EXPIRATION_MINUTES = 5;
    private readonly int MAX_FAILED_ATTEMPTS = 5;
    private readonly int LOCKOUT_DURATION_MINUTES = 15;


    public async Task<ErrorOr<LoginResponse>> LoginAsync(LoginRequest loginRequest)
    {
        // 先根據 username 或 email 查詢使用者
        var user = await userRepository.GetByUsernameOrEmailAsync(loginRequest.Login);
        if (user == null)
        {
            return Error.NotFound("UserNotFound", "The provided username or email does not exist.");
        }

        // 驗證帳戶是否被封鎖
        if (user.LoginLockoutEnd.HasValue && user.LoginLockoutEnd.Value > DateTime.UtcNow)
        {
            return Error.Unauthorized("UserLoginLocked", "The user account is locked.");
        }

        // 使用 BCrypt 比對密碼
        if (!BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.PasswordHash))
        {
            user.LoginFailedAttempts++;

            if (user.LoginFailedAttempts >= MAX_FAILED_ATTEMPTS)
            {
                user.LoginFailedAttempts = 0;
                user.LoginLockoutEnd = DateTime.UtcNow.AddMinutes(LOCKOUT_DURATION_MINUTES);
            }

            await userRepository.UpdateUserAsync(user);
            return Error.Unauthorized("InvalidPassword", "The provided password is incorrect.");
        }

        // 清除登入失敗次數 & 更新最後登入時間
        user.LoginFailedAttempts = 0;
        user.LastLoginDate = DateTime.UtcNow;
        await userRepository.UpdateUserAsync(user);

        var response = new LoginResponse
        {
            AccessToken = jwtUtils.GenerateToken(user.Id, user.Username, user.Email),
            RefreshToken = jwtUtils.GenerateRefreshToken()
        };

        return response;
    }

    public async Task<ErrorOr<LoginResponse>> RegisterAsync(RegisterRequest registerRequest)
    {
        // 檢查使用者名稱是否已經存在
        var existingUsername = await userRepository.GetUserByUsernameAsync(registerRequest.Username);
        if (existingUsername != null)
        {
            return Error.Conflict("UsernameAlreadyExists", "A user with the same username already exists.");
        }

        // 檢查 Email 是否已經存在
        var existingEmail = await userRepository.GetUserByEmailAsync(registerRequest.Email);
        if (existingEmail != null)
        {
            return Error.Conflict("EmailAlreadyExists", "A user with the same email already exists.");
        }

        // 密碼加密
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerRequest.Password);

        // 建立新使用者
        var user = new User
        {
            Username = registerRequest.Username,
            Email = registerRequest.Email,
            PasswordHash = hashedPassword,
            LastLoginDate = DateTime.UtcNow
        };

        // 儲存使用者
        await userRepository.AddUserAsync(user);

        // 生成登入回應
        var response = new LoginResponse
        {
            AccessToken = jwtUtils.GenerateToken(user.Id, user.Username, user.Email),
            RefreshToken = jwtUtils.GenerateRefreshToken()
        };

        return response;
    }


    public async Task<ErrorOr<ApiResponse>> ChangePasswordAsync(ChangePasswordRequest changePasswordRequest)
    {
        var user = await userRepository.GetUserByIdAsync(changePasswordRequest.UserId);
        if (user == null)
        {
            return Error.NotFound("UserNotFound", "The provided user does not exist.");
        }

        if (!BCrypt.Net.BCrypt.Verify(changePasswordRequest.OldPassword, user.PasswordHash))
        {
            return Error.Unauthorized("InvalidPassword", "The provided old password is incorrect.");
        }

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(changePasswordRequest.NewPassword);
        user.PasswordHash = hashedPassword;
        await userRepository.UpdateUserAsync(user);

        return ApiResponse.Success("Password changed successfully.");
    }

    public async Task<ErrorOr<ApiResponse>> ForgotPasswordAsync(ForgotPasswordRequest forgotPasswordRequest)
    {

        var user = await userRepository.GetUserByEmailAsync(forgotPasswordRequest.Email);

        if (user == null)
        {
            return Error.NotFound("UserNotFound", "The provided user does not exist.");
        }

        // 產生 Token 並記錄失效時間
        var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(256));

        user.ResetPasswordToken = token;
        user.ResetPasswordTokenExpiration = DateTime.UtcNow.AddMinutes(RESET_PASSWORD_TOKEN_EXPIRATION_MINUTES);

        await userRepository.UpdateUserAsync(user);

        // 發送修改密碼連結給使用者
        var resetLink = $"{config.GetValue<string>("FRONTEND_BASE_URL")}/reset-password?token={token}";
        var subject = "Password Reset Request";
        var body = $"Click on the link to reset your password: {resetLink}";

        await mailService.SendEmailWithErrorHandlingAsync(user.Email, subject, body);

        return ApiResponse.Success("A password reset email has been sent. Please check your inbox.");
    }

    public async Task<ErrorOr<ApiResponse>> ResetPasswordAsync(ResetPasswordRequest resetPasswordRequest)
    {
        var user = await userRepository.GetUserByIdAsync(resetPasswordRequest.UserId);

        if (user == null)
        {
            return Error.NotFound("UserNotFound", "The provided user does not exist.");
        }

        // 驗證帳戶是否被封鎖
        if (user.ResetPasswordLockoutEnd.HasValue && user.ResetPasswordLockoutEnd.Value > DateTime.UtcNow)
        {
            return Error.Unauthorized("UserResetPasswordLocked", "The user account is locked.");
        }

        // 驗證 Token 是否正確
        if (resetPasswordRequest.Token != user.ResetPasswordToken ||
            user.ResetPasswordTokenExpiration < DateTime.UtcNow)
        {
            user.ResetPasswordFailedAttempts++;

            if (user.ResetPasswordFailedAttempts >= MAX_FAILED_ATTEMPTS)
            {
                user.ResetPasswordFailedAttempts = 0;
                user.ResetPasswordLockoutEnd = DateTime.UtcNow.AddMinutes(LOCKOUT_DURATION_MINUTES);
            }

            await userRepository.UpdateUserAsync(user);
            return Error.Unauthorized("UserResetTokenInvalid", "The provided token is invalid or has expired.");
        }

        // 更新使用者的密碼
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(resetPasswordRequest.NewPassword);
        user.PasswordHash = hashedPassword;

        // 清除重設密碼的 Token 和失敗次數
        user.ResetPasswordToken = null;
        user.ResetPasswordFailedAttempts = 0;

        await userRepository.UpdateUserAsync(user);

        return ApiResponse.Success("Password reseted successfully.");
    }

    // Google 登入或註冊
    public async Task<ErrorOr<LoginResponse>> GoogleLoginAsync(string googleToken)
    {
        // 1. 驗證 Google Token 並解碼
        var payload = await ValidateGoogleTokenAsync(googleToken);
        if (payload == null)
        {
            return Error.Unauthorized("InvalidGoogleToken", "The provided Google token is invalid.");
        }

        var email = payload["email"]?.ToString();
        var name = payload["name"]?.ToString();

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(name))
        {
            return Error.Unauthorized("GoogleTokenInvalid", "The Google token did not contain valid email or name.");
        }

        // 2. 查詢該 email 是否已註冊
        var user = await userRepository.GetUserByEmailAsync(email);
        if (user == null)
        {
            // 如果沒有找到使用者，則創建新使用者
            user = new User
            {
                Username = name,
                Email = email,
                PasswordHash = "",  // Google 登入不需要密碼
            };
            await userRepository.AddUserAsync(user);
        }

        // 3. 生成 JWT Token 並返回
        var jwtToken = jwtUtils.GenerateToken(user.Id, user.Username, user.Email);
        var response = new LoginResponse
        {
            AccessToken = jwtToken,
            RefreshToken = jwtUtils.GenerateRefreshToken()
        };

        return response;
    }


    // 驗證 Google Token 的方法
    private async Task<JObject> ValidateGoogleTokenAsync(string googleToken)
    {
        // 使用 Google API 驗證 Token，並返回 payload (此處僅示範結構)
        // 你可以使用 Google 的 TokenInfo endpoint 或 Google API 客戶端來驗證 token
        var client = new HttpClient();
        var response = await client.GetStringAsync($"https://oauth2.googleapis.com/tokeninfo?id_token={googleToken}");

        // 回傳解碼後的 Google Token 內容 (payload)
        return JObject.Parse(response);
    }

}
