using lan_side_project.DTOs.Reponses.Auth;
using lan_side_project.Services;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Net.Http;
using Google.Apis.Auth;
using lan_side_project.DTOs.Requests.Auth;

namespace lan_side_project.Controllers;

[ApiController]
[Route("api/v1/auth")]
public class AuthController(AuthService authService) : BaseController
{
    /// <summary>
    /// 註冊
    /// </summary>
    /// <param name="registerRequest"></param>
    /// <returns></returns>
    [HttpPost("register")]
    public async Task<ActionResult<LoginResponse>> RegisterAsync(RegisterRequest registerRequest)
    {
        var result = await authService.RegisterAsync(registerRequest);
        return ErrorOrOkResponse(result);
    }

    /// <summary>
    /// 一般登入
    /// </summary>
    /// <param name="loginRequest"></param>
    /// <returns></returns>
    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> LoginAsync(LoginRequest loginRequest)
    {
        var result = await authService.LoginAsync(loginRequest);
        return ErrorOrOkResponse(result);
    }

    /// <summary>
    /// 使用 Google 帳號登入
    /// </summary>
    /// <param name="googleToken"></param>
    /// <returns></returns>
    [HttpPost("google-login")]
    public async Task<ActionResult<LoginResponse>> GoogleLoginAsync(GoogleLoginRequest request)
    {

        return Ok();
    }

    /// <summary>
    /// 忘記密碼
    /// </summary>
    /// <param name="forgotPasswordRequest"></param>
    /// <returns></returns>
    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPasswordAsync(ForgotPasswordRequest forgotPasswordRequest)
    {
        return Ok();
    }

    /// <summary>
    /// 重製密碼
    /// </summary>
    /// <param name="resetPasswordRequest"></param>
    /// <returns></returns>
    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPasswordAsync(ResetPasswordRequest resetPasswordRequest)
    {
        return Ok();
    }

    /// <summary>
    /// 更改密碼
    /// </summary>
    /// <param name="changePasswordRequest"></param>
    /// <returns></returns>
    [Authorize]
    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePasswordAsync(ChangePasswordRequest changePasswordRequest)
    {
        return Ok();
    }

    /// <summary>
    /// 跳轉到 Google 進行授權
    /// </summary>
    /// <returns></returns>
    [HttpGet("google-auth")]
    public IActionResult GoogleAuth()
    {
        var redirectUrl = Url.Action("GoogleCallback", "Auth");
        var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
        return Challenge(properties, GoogleDefaults.AuthenticationScheme);
    }

    /// <summary>
    /// Google 授權後的回調
    /// </summary>
    /// <returns></returns>
    [HttpGet("google-callback")]
    public async Task<IActionResult> GoogleCallback()
    {
        var result = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
        if (!result.Succeeded)
        {
            return BadRequest("Google authentication failed.");
        }

        // 在這裡處理用戶信息，例如創建或更新用戶
        return Ok();
    }
}
