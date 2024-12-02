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
    [MinLength(8)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
        ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character.")]
    [DataType(DataType.Password)]
    public required string Password { get; set; }
}