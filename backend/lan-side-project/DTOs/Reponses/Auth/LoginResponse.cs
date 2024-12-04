namespace lan_side_project.DTOs.Reponses.Auth;

public class LoginResponse
{
    public required string AccessToken { get; set; }
    public required string RefreshToken { get; set; }
}
