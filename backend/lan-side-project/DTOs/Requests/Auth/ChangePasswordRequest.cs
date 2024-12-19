using System.ComponentModel.DataAnnotations;

namespace lan_side_project.DTOs.Requests.Auth;

public class ChangePasswordRequest
{
    [Required]
    public required int UserId { get; set; }
    [Required]
    public required string OldPassword { get; set; }
    [Required]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
    [DataType(DataType.Password)]
    public required string NewPassword { get; set; }
}