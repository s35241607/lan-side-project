namespace lan_side_project.DTOs.Requests.User;

public class LoginRequest
{
    public required string Login { get; set; }
    public required string Password { get; set; }
}
