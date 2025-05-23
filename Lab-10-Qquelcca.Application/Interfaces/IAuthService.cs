using Lab_10_Qquelcca.Application.DTOs.Auth;
using System.Threading.Tasks;

namespace Lab_10_Qquelcca.Application.Interfaces;

public interface IAuthService
{
    Task<bool> RegisterAsync(RegisterRequest request);
    Task<LoginResponse> LoginAsync(LoginRequest request);

}