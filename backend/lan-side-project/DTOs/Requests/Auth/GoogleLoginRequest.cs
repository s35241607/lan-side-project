using System.ComponentModel.DataAnnotations;

namespace lan_side_project.DTOs.Requests.Auth;

public class GoogleLoginRequest
{
    [Required]
    public required string Token { get; set; }
}
