using Lab_10_Qquelcca.Application.DTOs.Role;
using Lab_10_Qquelcca.Application.Interfaces;
using Lab_10_Qquelcca.Domain.Entities;
using Lab_10_Qquelcca.Domain.Interfaces;
using System;
using System.Threading.Tasks;
using Lab_10_Qquelcca.Domain.Interfaces.Repositories;

namespace Lab_10_Qquelcca.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<bool> CreateRoleAsync(CreateRoleRequest request)
        {
            var existingRole = await _roleRepository.GetByNameAsync(request.RoleName);
            if (existingRole != null)
            {
                return false; // ya existe
            }

            var role = new Role()
            {
                RoleId = Guid.NewGuid(),
                RoleName = request.RoleName,
                CreatedAt = DateTime.UtcNow
            };

            await _roleRepository.AddAsync(role);
            await _roleRepository.SaveChangesAsync();

            return true;
        }
    }
}