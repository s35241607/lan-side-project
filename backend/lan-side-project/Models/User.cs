namespace lan_side_project.Models;

public class User : AuditBase
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    // 登錄
    public int LoginFailedAttempts { get; set; }
    public DateTime? LoginLockoutEnd { get; set; }
    public DateTime? LastLoginDate { get; set; }
    // 密碼變更紀錄
    public DateTime? LastPasswordChangedDate { get; set; }
    public DateTime? LastPasswordResetDate { get; set; }
    // 重設密碼
    public string? ResetPasswordToken { get; set; }
    public DateTime? ResetPasswordTokenExpiration { get; set; }
    public int ResetPasswordFailedAttempts { get; set; }
    public DateTime? ResetPasswordLockoutEnd { get; set; }

    public List<Role> Roles { get; } = [];
}
