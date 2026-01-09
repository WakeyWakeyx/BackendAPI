using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using WakeyWakeyBackendAPI.Models;

namespace WakeyWakeyBackendAPI.Services;

public class JwtService
{
    private readonly IConfiguration _config;

    public JwtService(IConfiguration config)
    {
        _config = config;
    }
    
    public string CreateToken(User user)
    { 
        var secretKey = _config["Jwt:Secret"]!;
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var descriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
            [
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            ]),
            Expires = DateTime.UtcNow.AddMinutes(_config.GetValue<int>("Jwt:ExpireMinutes")),
            SigningCredentials = credentials,
            Issuer = _config["Jwt:Issuer"],
            Audience = _config["Jwt:Audience"],
        };
        var handler = new JsonWebTokenHandler();
        var token = handler.CreateToken(descriptor);
        return token;
    }
}
