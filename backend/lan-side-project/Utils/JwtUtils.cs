using lan_side_project.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace lan_side_project.Utils;

public class JwtUtils(IConfiguration configuration)
{
    public string GenerateToken(int userId, string username, string email)
    {
        var issuer = configuration.GetValue<string>("JwtSettings:Issuer");
        var secretKey = configuration.GetValue<string>("JwtSettings:SecretKey") ?? throw new ArgumentNullException("JWT SecretKey not setting");
        var expireMinutes = configuration.GetValue<int>("JwtSettings:AccessTokenExpirationMinutes");

        var tokenHandler = new JwtSecurityTokenHandler();

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new(JwtRegisteredClaimNames.Name, username),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Email, email)
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = issuer,
            NotBefore = DateTime.UtcNow,
            IssuedAt = DateTime.UtcNow,
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(expireMinutes),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    public string GenerateRefreshToken() => Guid.NewGuid().ToString();
    
}

