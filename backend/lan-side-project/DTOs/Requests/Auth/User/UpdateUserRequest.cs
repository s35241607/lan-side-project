namespace lan_side_project.DTOs.Requests.Auth.User;

public class UpdateUserRequest
{
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}
