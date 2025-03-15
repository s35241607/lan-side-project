using AutoMapper;
using ErrorOr;
using lan_side_project.DTOs.Requests.Auth;
using lan_side_project.DTOs.Requests.Auth.User;
using lan_side_project.DTOs.Responses.User;
using lan_side_project.Models;
using lan_side_project.Repositories;
using lan_side_project.Utils;
using Microsoft.EntityFrameworkCore;

namespace lan_side_project.Services;

public class UserService(UserRepository userRepository)
{
    public async Task<ErrorOr<List<UserResponse>>> GetAllUsersAsync()
    {
        var users = await userRepository.GetAsync(include: q => q.Include(p => p.Roles));

        return MapperUtils.Mapper.Map<List<UserResponse>>(users);
    }

    public async Task<ErrorOr<UserResponse>> GetUserByIdAsync(int id)
    {
        var user = await userRepository.GetByIdAsync(id);

        return MapperUtils.Mapper.Map<UserResponse>(user);
    }

    public async Task<ErrorOr<UserResponse>> GetUserByUsernameAsync(string username)
    {
        var user = await userRepository.GetByUsernameOrEmailAsync(username);

        return MapperUtils.Mapper.Map<UserResponse>(user);
    }

    public async Task<ErrorOr<UserResponse>> CreateUserAsync(CreateUserRequest createUserRequest)
    {
        // 檢查使用者名稱是否已經存在
        var existingUsername = await userRepository.GetUserByUsernameAsync(createUserRequest.Username);
        if (existingUsername != null)
        {
            return Error.Conflict("UsernameAlreadyExists", "A user with the same username already exists.");
        }

        // 檢查 Email 是否已經存在
        var existingEmail = await userRepository.GetUserByEmailAsync(createUserRequest.Email);
        if (existingEmail != null)
        {
            return Error.Conflict("EmailAlreadyExists", "A user with the same email already exists.");
        }

        // 密碼加密
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(createUserRequest.Password);

        // 建立新使用者
        var user = new User
        {
            Username = createUserRequest.Username,
            Email = createUserRequest.Email,
            PasswordHash = hashedPassword,
            LastLoginDate = DateTime.UtcNow
        };

        // 儲存使用者
        await userRepository.AddAsync(user);

        return MapperUtils.Mapper.Map<UserResponse>(user);
    }

    public async Task<ErrorOr<UserResponse>> UpdateUserAsync(int id, UpdateUserRequest updateUserRequest)
    {
        var user = await userRepository.GetByIdAsync(id);
        if (user == null)
        {
            return Error.NotFound("UserNotFound", "The provided user does not exist.");
        }

        // 檢查使用者名稱是否已經存在
        var existingUsername = await userRepository.GetUserByUsernameAsync(updateUserRequest.Username);
        if (existingUsername != null)
        {
            return Error.Conflict("UsernameAlreadyExists", "A user with the same username already exists.");
        }

        // 檢查 Email 是否已經存在
        var existingEmail = await userRepository.GetUserByEmailAsync(updateUserRequest.Email);
        if (existingEmail != null)
        {
            return Error.Conflict("EmailAlreadyExists", "A user with the same email already exists.");
        }

        // 密碼加密
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(updateUserRequest.Password);

        // 修改使用者
        user.Username = updateUserRequest.Username;
        user.Email = updateUserRequest.Email;
        user.PasswordHash = hashedPassword;

        await userRepository.UpdateAsync(user);

        return MapperUtils.Mapper.Map<UserResponse>(user);
    }
}
