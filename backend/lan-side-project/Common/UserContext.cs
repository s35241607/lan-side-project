using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace lan_side_project.Common;
public interface IUserContext
{
    ClaimsPrincipal User { get; }
    int UserId { get; }
    string UserName { get; }
    bool IsAuthenticated { get; }
    bool IsInRole(string role);
}

public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
    public ClaimsPrincipal User { get; } = httpContextAccessor?.HttpContext?.User ?? new ClaimsPrincipal();

    // 取得使用者的 ID (假設有 "sub" claim)
    public int UserId => int.TryParse(User?.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId) ? userId : 0;
    
    // 取得使用者的名稱 (假設有 "name" claim)
    public string UserName => User?.Identity?.Name ?? "None";

    // 檢查使用者是否已驗證
    public bool IsAuthenticated => User?.Identity?.IsAuthenticated ?? false;

    // 檢查使用者是否擁有某個角色
    public bool IsInRole(string role)
    {
        // 檢查 User 和角色是否為 null
        return User?.IsInRole(role) ?? false;
    }
}
