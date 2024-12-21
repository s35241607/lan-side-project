using lan_side_project.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace lan_side_project.Controllers;

[ApiController]
[Route("api/test")]
public class TestController(MailService mailService) : BaseController
{
    [HttpPost("send-mail")]
    public async Task<IActionResult> SendMailAsync(string to, string subject, string body)
    {
        await mailService.SendEmailAsync(to, subject, body);
        return Ok("Send successfully");
    }

    [Authorize]
    [HttpGet("validate-jwt")]
    public async Task<IActionResult> ValidateAsync()
    {
        return Ok("success");
    }
}
