namespace lan_side_project.DTOs.Requests.User;

public class ResetPasswordRequest
{
    public required string Token { get; set; }
    public required string NewPassword { get; set; }
}