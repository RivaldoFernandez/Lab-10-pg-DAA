// using Lab_10_Qquelcca.Domain.Entities;
// using Lab_10_Qquelcca.Domain.Interfaces.Repositories;
// using Lab_10_Qquelcca.Infrastructure.Context;
// using Microsoft.EntityFrameworkCore;
//
// namespace Lab_10_Qquelcca.Infrastructure.Repositories
// {
//     public class RoleRepository : IRoleRepository
//     {
//         private readonly ApplicationDbContext _context;
//
//         public RoleRepository(ApplicationDbContext context)
//         {
//             _context = context;
//         }
//
//         // Implementa aquí los métodos definidos en IRoleRepository
//         // Por ejemplo:
//
//         public async Task<Role> GetByIdAsync(Guid id)
//         {
//             return await _context.Roles.FindAsync(id);
//         }
//
//         // Otros métodos según la interfaz...
//     }
// }
using Lab_10_Qquelcca.Domain.Entities;
using Lab_10_Qquelcca.Domain.Interfaces;
using Lab_10_Qquelcca.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Lab_10_Qquelcca.Domain.Interfaces.Repositories;

namespace Lab_10_Qquelcca.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Role role)
        {
            await _context.Roles.AddAsync(role);
        }

        public async Task<Role?> GetByIdAsync(Guid roleId)
        {
            return await _context.Roles.FindAsync(roleId);
        }

        public async Task<Role?> GetByNameAsync(string roleName)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.RoleName == roleName);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
