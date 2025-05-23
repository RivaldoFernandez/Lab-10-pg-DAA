namespace Lab_10_Qquelcca.Application.DTOs.Auth;

public class RegisterRequest
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string? Email { get; set; }
}
