namespace lan_side_project.Models
{
    public class Role : AuditBase
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public ICollection<Permission> Permissions { get; set; } = [];
    }
}
