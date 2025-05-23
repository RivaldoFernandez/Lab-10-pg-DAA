using Lab_10_Qquelcca.Domain.Entities;
using Lab_10_Qquelcca.Domain.Interfaces.Repositories;
using Lab_10_Qquelcca.Infrastructure.Context;

namespace Lab_10_Qquelcca.Infrastructure.Repositories;


using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;


public class UserRoleRepository : IUserRoleRepository
{
    private readonly ApplicationDbContext _context;

    public UserRoleRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(UserRole userRole)
    {
        await _context.UserRoles.AddAsync(userRole);
    }

    public async Task<bool> ExistsAsync(Guid userId, Guid roleId)
    {
        return await _context.UserRoles.AnyAsync(ur => ur.UserId == userId && ur.RoleId == roleId);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
