using Lab_10_Qquelcca.Application.DTOs.UserRole;

namespace Lab_10_Qquelcca.Application.Services;

using Lab_10_Qquelcca.Application.DTOs;
using Lab_10_Qquelcca.Domain.Entities;
using Lab_10_Qquelcca.Domain.Interfaces.Repositories;
using System;
using System.Threading.Tasks;
public class UserRoleService
{
    private readonly IUserRoleRepository _userRoleRepository;

    public UserRoleService(IUserRoleRepository userRoleRepository)
    {
        _userRoleRepository = userRoleRepository;
    }

    public async Task AssignRoleAsync(UserRoleAssignDto dto)
    {
        if (await _userRoleRepository.ExistsAsync(dto.UserId, dto.RoleId))
            throw new Exception("Este rol ya está asignado a este usuario.");

        var userRole = new UserRole
        {
            UserId = dto.UserId,
            RoleId = dto.RoleId,
            AssignedAt = DateTime.UtcNow
        };

        await _userRoleRepository.AddAsync(userRole);
        await _userRoleRepository.SaveChangesAsync();
    }
}

