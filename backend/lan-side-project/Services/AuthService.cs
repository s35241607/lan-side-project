using ErrorOr;
using lan_side_project.DTOs.Reponses;
using lan_side_project.DTOs.Requests.User;
using lan_side_project.Repositories;
using lan_side_project.Utils;

namespace lan_side_project.Services;

public class AuthService(UserRepository userRepository, JwtUtils jwtUtils)
{
    public async Task<ErrorOr<LoginResponse>> LoginAsync(LoginRequest loginRequest)
    {
        // 先根據 username 或 email 查詢使用者
        var user = await userRepository.GetByUsernameOrEmailAsync(loginRequest.Login);
        if (user == null)
        {
            return Error.NotFound("User or email not found.");
        }

        // 使用 BCrypt 比對密碼
        if (!BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.PasswordHash))
        {
            return Error.Unauthorized("Incorrect password, please try again or click 'Forgot Password' to reset your password.");
        }

        var response = new LoginResponse
        {
            AccessToken = jwtUtils.GenerateToken(user.Id, user.Username, user.Email),
            RefreshToken = jwtUtils.GenerateRefreshToken()
        };

        return response;
    }
}
