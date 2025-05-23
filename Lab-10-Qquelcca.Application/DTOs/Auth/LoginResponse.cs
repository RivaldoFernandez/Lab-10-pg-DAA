namespace Lab_10_Qquelcca.Application.DTOs.Auth;

public class LoginResponse
{
    public bool Success { get; set; }
    public string Token { get; set; }  // Si usas JWT
    public string Message { get; set; }
}
