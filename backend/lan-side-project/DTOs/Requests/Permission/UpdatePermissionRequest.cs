using System.ComponentModel.DataAnnotations;

namespace lan_side_project.DTOs.Requests.Permission;

public class UpdatePermissionRequest
{
    [Required]
    public required string Name { get; set; }
    public string? Description { get; set; }
}
