using lan_side_project.Models;

namespace lan_side_project.DTOs.Responses.User;

public record UserResponse(
    int Id,
    string Username,
    string Email,
    int LoginFailedAttempts,
    DateTime? LoginLockoutEnd,
    DateTime? LastLoginDate,
    List<Models.Role> Roles
);
