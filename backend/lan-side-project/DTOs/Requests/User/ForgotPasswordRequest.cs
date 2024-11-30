using System.ComponentModel.DataAnnotations;

namespace lan_side_project.DTOs.Requests.User;

public class ForgotPasswordRequest
{
    [Required]
    public required string Email { get; set; }
}
