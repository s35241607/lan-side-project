using System.ComponentModel.DataAnnotations;

namespace lan_side_project.DTOs.Requests.Auth;

public class LoginRequest
{
    [Required]
    public required string Login { get; set; }
    [Required]
    public required string Password { get; set; }
}
