using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using WakeyWakeyBackendAPI.Models;

namespace WakeyWakeyBackendAPI.Services;

/// <summary>
/// A simple utility service for producing JWT tokens. 
/// </summary>
public class JwtService
{
    private readonly IConfiguration _config;

    public JwtService(IConfiguration config)
    {
        _config = config;
    }
    
    /// <summary>
    /// Creates a new JWT token for a given user.
    /// </summary>
    /// <param name="user">the user to generate a jwt token for.</param>
    /// <returns>the jwt token as a string.</returns>
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
            Expires = DateTime.UtcNow.AddMinutes(_config.GetValue<int>("Jwt:ExpirationInMinutes")),
            SigningCredentials = credentials,
            Issuer = _config["Jwt:Issuer"],
            Audience = _config["Jwt:Audience"],
        };
        var handler = new JsonWebTokenHandler();
        var token = handler.CreateToken(descriptor);
        return token;
    }
}
