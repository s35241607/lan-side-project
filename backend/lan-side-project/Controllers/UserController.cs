using lan_side_project.Common;
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
    /// 取得當前使用者資訊
    /// </summary>
    /// <returns></returns>

    [HttpGet("me")]
    public async Task<ActionResult<UserResponse>> GetUserByCurrentUserAsync()
    {
        var result = await userService.GetUserByIdAsync(userContext.UserId);
        return ErrorOrOk(result);
    }
}
