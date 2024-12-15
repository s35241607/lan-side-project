using lan_side_project.DTOs.Reponses.User;
using lan_side_project.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace lan_side_project.Controllers;

[Route("api/v1/users")]
[ApiController]
public class UserController(UserService userService) : BaseController
{
    [HttpGet]
    public async Task<ActionResult<List<UserResponse>>> GetAllUsersAsync()
    {
        var result = await userService.GetAllUsersAsync();
        return ErrorOrOkResponse(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserResponse>> GetUserByIdAsync(int id)
    {
        var result = await userService.GetUserByIdAsync(id);
        return ErrorOrOkResponse(result);
    }

    [HttpGet("{id}/iamge")]
    public async Task<ActionResult> GetUserImageByIdAsync(int id)
    {
        // TODO: 取得圖片
        var result = "";
        return ErrorOrNoContent(result);
    }

    [HttpPost("{id}/iamge")]
    public async Task<ActionResult> UploadUserImageAsync(int id, IFormFile image)
    {
        // TODO: 上傳圖片
        var result = "";
        return ErrorOrNoContent(result);
    }
}
