﻿using lan_side_project.DTOs.Responses.Auth;
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
using lan_side_project.DTOs.Responses;

namespace lan_side_project.Controllers;

[ApiController]
[Route("api/v1/auth")]
public class AuthController(AuthService authService) : BaseController
{
    /// <summary>
    /// 註冊新帳戶
    /// </summary>
    /// <param name="registerRequest">註冊請求資料</param>
    /// <returns>註冊結果</returns>
    [HttpPost("register")]
    public async Task<ActionResult<LoginResponse>> RegisterAsync(RegisterRequest registerRequest)
    {
        var result = await authService.RegisterAsync(registerRequest);
        return ErrorOrOk(result);
    }

    /// <summary>
    /// 一般登入
    /// </summary>
    /// <param name="loginRequest">登入請求資料</param>
    /// <returns>登入結果</returns>
    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> LoginAsync(LoginRequest loginRequest)
    {
        var result = await authService.LoginAsync(loginRequest);
        return ErrorOrOk(result);
    }

    /// <summary>
    /// 更改使用者密碼
    /// </summary>
    /// <param name="changePasswordRequest">更改密碼請求資料</param>
    /// <returns>更改密碼結果</returns>
    [Authorize]
    [HttpPost("change-password")]
    public async Task<ActionResult<ApiResponse>> ChangePasswordAsync(ChangePasswordRequest changePasswordRequest)
    {
        var result = await authService.ChangePasswordAsync(changePasswordRequest);
        return ErrorOrOk(result);
    }

    /// <summary>
    /// 忘記密碼流程
    /// </summary>
    /// <param name="forgotPasswordRequest">忘記密碼請求資料</param>
    /// <returns>忘記密碼結果</returns>
    [HttpPost("forgot-password")]
    public async Task<ActionResult<ApiResponse>> ForgotPasswordAsync(ForgotPasswordRequest forgotPasswordRequest)
    {
        var result = await authService.ForgotPasswordAsync(forgotPasswordRequest);
        return ErrorOrOk(result);
    }

    /// <summary>
    /// 重製密碼
    /// </summary>
    /// <param name="resetPasswordRequest">重製密碼請求資料</param>
    /// <returns>重製密碼結果</returns>
    [HttpPost("reset-password")]
    public async Task<ActionResult<ApiResponse>> ResetPasswordAsync(ResetPasswordRequest resetPasswordRequest)
    {
        var result = await authService.ResetPasswordAsync(resetPasswordRequest);
        return ErrorOrOk(result);
    }

    /// <summary>
    /// 跳轉到 Google 進行授權
    /// </summary>
    /// <returns></returns>
    [HttpGet("google-auth")]
    public IActionResult GoogleAuth([FromQuery] string returnUrl = "/")
    {
        var redirectUrl = Url.Action("GoogleCallback", "Auth", new { returnUrl });
        var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
        return Challenge(properties, GoogleDefaults.AuthenticationScheme);
    }

    /// <summary>
    /// Google 授權後的回調
    /// </summary>
    /// <returns></returns>
    [HttpGet("google-callback")]
    public async Task<IActionResult> GoogleCallback(string returnUrl)
    {
        var result = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);

        if (!result.Succeeded)
        {
            return BadRequest("Google authentication failed.");
        }

        // 使用者的資料會包含在 result.Principal 中，這裡可以提取到用戶資訊
        var claims = result.Principal?.Claims.Select(c => new { c.Type, c.Value });

        // 假設你想根據 Google 賬戶信息生成 JWT
        var jwtToken = "google";

        // 返回 token 和 returnUrl 給前端
        //return Redirect($"{returnUrl}?token={jwtToken}");
        // 統一回應格式，返回 token 和 returnUrl
        return Ok(new { Token = jwtToken, ReturnUrl = returnUrl });
    }
}
