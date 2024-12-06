namespace lan_side_project.Models;

public abstract class AuditBase
{
    public int CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public int? UpdatedBy { get; set;}
    public DateTime? UpdatedAt { get; set;}
}
