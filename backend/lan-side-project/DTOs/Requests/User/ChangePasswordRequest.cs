using System.ComponentModel.DataAnnotations;

namespace lan_side_project.DTOs.Requests.User;

public class ChangePasswordRequest
{
    [Required]
    public required string OldPassword { get; set; }
    [Required]
    public required string NewPassword { get; set; }
}