using System.ComponentModel.DataAnnotations;

namespace lan_side_project.DTOs.Requests.User;

public class RegisterRequest
{
    [Required]
    [EmailAddress(ErrorMessage = "Email format is incorrect.")]
    public required string Email { get; set; }
    [Required]
    public required string Username { get; set; }
    [Required]
    public required string Password { get; set; }
}