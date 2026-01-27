namespace WakeyWakeyBackendAPI.DTOs;

public class RegisterUserResponseDto
{
    public required string Email { get; set; }
    public required string Name { get; set; }
    public required string JwtToken { get; set; }
}