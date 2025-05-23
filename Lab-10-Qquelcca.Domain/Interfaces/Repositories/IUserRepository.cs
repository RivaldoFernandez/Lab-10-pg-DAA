

using Lab_10_Qquelcca.Domain.Entities;

namespace Lab_10_Qquelcca.Domain.Interfaces.Repositories;

public interface IUserRepository
{
    Task AddAsync(User user);
    Task<User?> GetByUsernameAsync(string username);
}