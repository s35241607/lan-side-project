﻿using lan_side_project.Common;
using lan_side_project.DTOs.Requests.Auth.User;
using lan_side_project.DTOs.Responses.User;
using lan_side_project.Repositories;
using lan_side_project.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace lan_side_project.Controllers;

[Route("api/v1/users")]
[ApiController]
[Authorize]
public class UserController(UserService userService, IUserContext userContext) : BaseController
{
    /// <summary>
    /// 取得所有使用者資料
    /// </summary>
    /// <returns></returns>

    [HttpGet]
    public async Task<ActionResult<List<UserResponse>>> GetAllUsersAsync()
    {
        var result = await userService.GetAllUsersAsync();
        return ErrorOrOk(result);
    }

    /// <summary>
    /// 依照 ID 取得使用者資料
    /// </summary>
    /// <param name="id">使用者 ID</param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<UserResponse>> GetUserByIdAsync(int id)
    {
        var result = await userService.GetUserByIdAsync(id);
        return ErrorOrOk(result);
    }

    /// <summary>
    /// 依照使用者名稱取得使用者資料
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>

    [HttpGet("/username/{username}")]
    public async Task<ActionResult<UserResponse>> GetUserByUsernameAsync(string username)
    {
        var result = await userService.GetUserByUsernameAsync(username);
        return ErrorOrOk(result);
    }

    /// <summary>
    /// 取得當前使用者資訊
    /// </summary>
    /// <returns></returns>

    [HttpGet("me")]
    public async Task<ActionResult<UserResponse>> GetUserByCurrentUserAsync()
    {
        var result = await userService.GetUserByIdAsync(userContext.UserId);
        return ErrorOrOk(result);
    }

    /// <summary>
    /// 建立新使用者
    /// </summary>
    /// <param name="createUserRequest"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<UserResponse>> CreateUserAsync(CreateUserRequest createUserRequest)
    {
        var result = await userService.CreateUserAsync(createUserRequest);
        return ErrorOrCreated(result, $"api/v1/users/{result.Value?.Id}");
    }

    /// <summary>
    /// 更新使用者資料
    /// </summary>
    /// <param name="id"></param>
    /// <param name="updateUserRequest"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<UserResponse>> UpdateUserAsync(int id, UpdateUserRequest updateUserRequest)
    {
        var result = await userService.UpdateUserAsync(id, updateUserRequest);
        return ErrorOrOk(result);
    }

}
