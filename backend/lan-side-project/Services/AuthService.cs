using ErrorOr;
using lan_side_project.DTOs.Reponses.Auth;
using lan_side_project.DTOs.Requests.Auth;
using lan_side_project.Models;
using lan_side_project.Repositories;
using lan_side_project.Utils;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace lan_side_project.Services;

public class AuthService(UserRepository userRepository, JwtUtils jwtUtils)
{
    public async Task<ErrorOr<LoginResponse>> LoginAsync(LoginRequest loginRequest)
    {
        // 先根據 username 或 email 查詢使用者
        var user = await userRepository.GetByUsernameOrEmailAsync(loginRequest.Login);
        if (user == null)
        {
            return Error.NotFound("UserNotFound", "The provided username or email does not exist.");
        }

        // 使用 BCrypt 比對密碼
        if (!BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.PasswordHash))
        {
            return Error.Unauthorized("InvalidPassword", "The provided password is incorrect.");
        }

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

        // TODO: 檢查信箱是否合法

        // 密碼加密
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerRequest.Password);

        // 建立新使用者
        var user = new User
        {
            Username = registerRequest.Username,
            Email = registerRequest.Email,
            PasswordHash = hashedPassword
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


    public async Task<ErrorOr<object>> ChangePasswordAsync(ChangePasswordRequest changePasswordRequest)
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

        return new { message = "Password changed successfully" };
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
