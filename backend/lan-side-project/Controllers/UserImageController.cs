using lan_side_project.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace lan_side_project.Controllers;

[Route("api/v1/users/")]
[ApiController]
[Authorize]

public class UserImageController(UserImageService userImageService) : BaseController
{
    /// <summary>
    /// 取得指定使用者的圖片
    /// </summary>
    /// <param name="id">使用者 ID</param>
    /// <returns></returns>

    [HttpGet("{id}/image")]
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
    /// <returns></returns>

    [HttpPost("{id}/image")]
    public async Task<ActionResult> UploadUserImageAsync(int id, IFormFile image)
    {
        // TODO: 上傳圖片
        var result = "";
        return ErrorOrNoContent(result);
    }
}
