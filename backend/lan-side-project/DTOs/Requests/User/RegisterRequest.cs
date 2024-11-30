namespace lan_side_project.DTOs.Requests.User;

public class RegisterRequest
{
    public required string Email { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
}