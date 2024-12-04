using System.ComponentModel.DataAnnotations;

namespace lan_side_project.DTOs.Requests.User;

public class ChangePasswordRequest
{
    [Required]
    public required string OldPassword { get; set; }
    [Required]
    [MinLength(8)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
        ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character.")]
    [DataType(DataType.Password)]
    public required string NewPassword { get; set; }
}