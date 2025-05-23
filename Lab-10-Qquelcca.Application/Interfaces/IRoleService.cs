using Lab_10_Qquelcca.Application.DTOs.Role;

namespace Lab_10_Qquelcca.Application.Interfaces;

public interface IRoleService
{
    Task<bool> CreateRoleAsync(CreateRoleRequest request);
}