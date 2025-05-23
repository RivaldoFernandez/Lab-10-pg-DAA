using Lab_10_Qquelcca.Application.DTOs;

namespace Lab_10_Qquelcca.Application.Interfaces;

public interface IUserService
{
    Task<Guid> RegisterAsync(CreateUserDto dto);
}