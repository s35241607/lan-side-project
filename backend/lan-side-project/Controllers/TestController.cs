using lan_side_project.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System.Text.Json;

namespace lan_side_project.Controllers;

[ApiController]
[Route("api/test")]
public class TestController(
    MailService mailService,
    IDistributedCache cache,
    IConnectionMultiplexer connectionMultiplexer) : BaseController
{
    [HttpPost("send-mail")]
    public async Task<IActionResult> SendMailAsync(string to, string subject, string body)
    {
        await mailService.SendEmailAsync(to, subject, body);
        return Ok("Send successfully");
    }

    [Authorize]
    [HttpGet("validate-jwt")]
    public IActionResult ValidateAsync()
    {
        return Ok("success");
    }

    [HttpPost("cache")]
    public async Task<IActionResult> SetCacheAsync()
    {
        var obj = new { Name = "Lan", Age = 18 };
        var json = JsonSerializer.Serialize(obj);

        await cache.SetStringAsync("test", json, new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(5)
        });

        return Ok("Set cache successfully");    
    }

    [HttpGet("cache")]
    public async Task<IActionResult> GetCacheAsync()
    {
        var json = await cache.GetStringAsync("test");
        if (json == null)
        {
            return NotFound("Cache not found");
        }

        var obj = JsonSerializer.Deserialize<Dictionary<string, object>>(json);
        if (obj == null)
        {
            return NotFound("Cache not found");
        }
        return Ok(obj);
    }

    [HttpGet("cache2")]
    public async Task<IActionResult> GetValueAsync(string key)
    {
        var db = connectionMultiplexer.GetDatabase();
        var result = await db.StringGetAsync(key);
        return Ok(result);
    }

    [HttpPost("cache2")]

    public async Task<IActionResult> SetValueAsync(string key, string value)
    {
        var db = connectionMultiplexer.GetDatabase();
        await db.StringSetAsync(key, value);
        return Ok("Set value successfully");
    }

}
