namespace lan_side_project.Models;

public class Role : AuditBase
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }

    public List<User> Users { get; } = [];

    public List<Permission> Permissions { get; } = [];
}
