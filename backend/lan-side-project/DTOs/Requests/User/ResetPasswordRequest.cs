using System.ComponentModel.DataAnnotations;

namespace lan_side_project.DTOs.Requests.User;

public class ResetPasswordRequest
{
    [Required]
    public required string Token { get; set; }
    [Required]
    public required string NewPassword { get; set; }
}