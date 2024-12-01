using ErrorOr;
using lan_side_project.DTOs.Reponses;
using lan_side_project.DTOs.Requests.User;
using lan_side_project.Models;
using lan_side_project.Repositories;
using lan_side_project.Utils;
using Microsoft.AspNetCore.Mvc;

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
}
