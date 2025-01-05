using System.ComponentModel.DataAnnotations;

namespace lan_side_project.DTOs.Requests.Role;

public class CreateRoleRequest()
{
    [Required]
    public required string Name { get; set; }
    public string? Description { get; set; }
}
