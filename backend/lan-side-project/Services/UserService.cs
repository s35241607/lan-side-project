﻿using AutoMapper;
using ErrorOr;
using lan_side_project.DTOs.Reponses.User;
using lan_side_project.Repositories;
using lan_side_project.Utils;

namespace lan_side_project.Services;

public class UserService(UserRepository userRepository)
{
    public async Task<ErrorOr<List<UserResponse>>> GetAllUsersAsync()
    {
        var users = await userRepository.GetAllUsersAsync();

        return MapperUtils.Mapper.Map<List<UserResponse>>(users);
    }

    public async Task<ErrorOr<UserResponse>> GetUserByIdAsync(int id)
    {
        var user = await userRepository.GetUserByIdAsync(id);

        return MapperUtils.Mapper.Map<UserResponse>(user);
    }
}
