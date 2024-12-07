﻿using lan_side_project.DTOs.Reponses.Auth;
using lan_side_project.DTOs.Requests.User;
using lan_side_project.Services;
using Microsoft.AspNetCore.Mvc;

namespace lan_side_project.Controllers;

[ApiController]
[Route("api/v1/auth")]
public class AuthController(AuthService authService) : BaseController
{
    [HttpPost("register")]
    public async Task<ActionResult<LoginResponse>> RegisterAsync(RegisterRequest registerRequest)
    {
        var result = await authService.RegisterAsync(registerRequest);

        return ErrorOrOkResponse(result);
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> LoginAsync(LoginRequest loginRequest)
    {
        var result = await authService.LoginAsync(loginRequest);

        return ErrorOrOkResponse(result);
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPasswordAsync(ForgotPasswordRequest forgotPasswordRequest)
    {
        return Ok();
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPasswordAsync(ResetPasswordRequest resetPasswordRequest)
    {
        return Ok();
    }

    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePasswordAsync(ChangePasswordRequest changePasswordRequest)
    {
        return Ok();
    }
}
