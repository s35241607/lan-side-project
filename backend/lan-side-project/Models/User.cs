namespace lan_side_project.Models;

public class User : AuditBase
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;

    public List<Role> Roles { get; } = [];
}
