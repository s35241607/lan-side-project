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
}
