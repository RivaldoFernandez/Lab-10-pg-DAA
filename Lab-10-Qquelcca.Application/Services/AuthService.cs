using Lab_10_Qquelcca.Application.DTOs.Auth;
using Lab_10_Qquelcca.Application.Interfaces;
using Lab_10_Qquelcca.Domain.Entities;
using Lab_10_Qquelcca.Domain.Interfaces;
using Lab_10_Qquelcca.Domain.Interfaces.Unit;
using Microsoft.AspNetCore.Identity;

namespace Lab_10_Qquelcca.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly PasswordHasher<User> _passwordHasher;

    public AuthService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = new PasswordHasher<User>(); // Lo puedes crear aquí directamente o inyectar si quieres
    }

    public async Task<bool> RegisterAsync(RegisterRequest request)
    {
        var user = new User()
        {
            UserId = Guid.NewGuid(),
            Username = request.Username,
            Email = request.Email,
            CreatedAt = DateTime.UtcNow
        };

        // Aquí pasas el objeto user y el password plano
        user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);

        await _unitOfWork.UserRepository.AddAsync(user);
        await _unitOfWork.SaveAsync();

        return true;
    }
}