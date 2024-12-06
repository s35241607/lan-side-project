using System.ComponentModel.DataAnnotations;

namespace lan_side_project.DTOs.Requests.User;

public class RegisterRequest
{
    [Required]
    [EmailAddress]
    [MaxLength(254)]
    public required string Email { get; set; }

    [Required]
    [StringLength(20, MinimumLength = 3)]
    [RegularExpression(@"^[a-zA-Z0-9_.]+$", ErrorMessage = "Username can only contain letters, numbers, underscores, and periods.")]
    public required string Username { get; set; }

    [Required]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
    [DataType(DataType.Password)]
    public required string Password { get; set; }
}