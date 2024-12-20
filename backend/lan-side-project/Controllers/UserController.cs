using lan_side_project.DTOs.Responses.User;
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
    /// <summary>
    /// 取得所有使用者資料
    /// </summary>
    /// <returns>回傳所有使用者的資料列表</returns>
    [HttpGet]
    public async Task<ActionResult<List<UserResponse>>> GetAllUsersAsync()
    {
        var result = await userService.GetAllUsersAsync();
        return ErrorOrOkResponse(result);
    }

    /// <summary>
    /// 依照 ID 取得使用者資料
    /// </summary>
    /// <param name="id">使用者 ID</param>
    /// <returns>回傳指定 ID 的使用者資料</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<UserResponse>> GetUserByIdAsync(int id)
    {
        var result = await userService.GetUserByIdAsync(id);
        return ErrorOrOkResponse(result);
    }

    /// <summary>
    /// 取得指定使用者的圖片
    /// </summary>
    /// <param name="id">使用者 ID</param>
    /// <returns>回傳使用者圖片</returns>

    [HttpGet("{id}/iamge")]
    public async Task<ActionResult> GetUserImageByIdAsync(int id)
    {
        // TODO: 取得圖片
        var result = "";
        return ErrorOrNoContent(result);
    }

    /// <summary>
    /// 上傳使用者圖片
    /// </summary>
    /// <param name="id">使用者 ID</param>
    /// <param name="image">上傳的圖片檔案</param>
    /// <returns>回傳圖片上傳結果</returns>

    [HttpPost("{id}/iamge")]
    public async Task<ActionResult> UploadUserImageAsync(int id, IFormFile image)
    {
        // TODO: 上傳圖片
        var result = "";
        return ErrorOrNoContent(result);
    }
}
