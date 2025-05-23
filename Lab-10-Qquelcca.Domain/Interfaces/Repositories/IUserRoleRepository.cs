namespace Lab_10_Qquelcca.Domain.Interfaces.Repositories;

using Lab_10_Qquelcca.Domain.Entities;
using System;
using System.Threading.Tasks;

public interface IUserRoleRepository
{
    Task AddAsync(UserRole userRole);
    Task SaveChangesAsync();
    Task<bool> ExistsAsync(Guid userId, Guid roleId);
}

