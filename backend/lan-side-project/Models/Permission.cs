namespace lan_side_project.Models;

public class Permission : AuditBase
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }

    public List<Role> Roles { get;  } = [];

}
