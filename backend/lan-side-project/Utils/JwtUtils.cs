using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace lan_side_project.Utils;

public class JwtUtils(IConfiguration configuration)
{
    public string GenerateToken(string userName, string email, int expireMinutes = 30)
    {
        var issuer = configuration.GetValue<string>("JwtSettings:Issuer");
        var secretKey = configuration.GetValue<string>("JwtSettings:SecretKey");

        // Configuring "Claims" to your JWT Token
        var claims = new List<Claim>
        {
            // In RFC 7519 (Section#4), there are defined 7 built-in Claims, but we mostly use 2 of them.
            //claims.Add(new Claim(JwtRegisteredClaimNames.Iss, issuer));
            new Claim(JwtRegisteredClaimNames.Sub, userName), // User.Identity.Name
            //new Claim(JwtRegisteredClaimNames.Aud, "The Audience");
            //new Claim(JwtRegisteredClaimNames.Exp, DateTimeOffset.UtcNow.AddMinutes(30).ToUnixTimeSeconds().ToString());
            //new Claim(JwtRegisteredClaimNames.Nbf, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()); // 必須為數字
            //new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()); // 必須為數字
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // JWT ID

            // The "NameId" claim is usually unnecessary.
            //new Claim(JwtRegisteredClaimNames.NameId, userName);

            // This Claim can be replaced by JwtRegisteredClaimNames.Sub, so it's redundant.
            //new Claim(ClaimTypes.Name, userName);

            // TODO: You can define your "roles" to your Claims.
            new Claim(ClaimTypes.Role, "Admin"),
            new Claim(ClaimTypes.Role, "Users"),
            new Claim(ClaimTypes.Email, email)
        };

        var userClaimsIdentity = new ClaimsIdentity(claims);

        // Create a SymmetricSecurityKey for JWT Token signatures
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

        // HmacSha256 MUST be larger than 128 bits, so the key can't be too short. At least 16 and more characters.
        // https://stackoverflow.com/questions/47279947/idx10603-the-algorithm-hs256-requires-the-securitykey-keysize-to-be-greater
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        // Create SecurityTokenDescriptor
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = issuer,
            //Audience = issuer, // Sometimes you don't have to define Audience.
            //NotBefore = DateTime.UtcNow, // Default is DateTime.Now
            //IssuedAt = DateTime.UtcNow, // Default is DateTime.Now
            Subject = userClaimsIdentity,
            Expires = DateTime.UtcNow.AddMinutes(expireMinutes),
            SigningCredentials = signingCredentials
        };

        // Generate a JWT securityToken, than get the serialized Token result (string)
        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        var serializeToken = tokenHandler.WriteToken(securityToken);

        return serializeToken;
    }
}

