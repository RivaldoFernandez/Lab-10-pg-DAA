using Lab_10_Qquelcca.Domain.Entities;

namespace Lab_10_Qquelcca.Domain.Interfaces.Repositories
{
    public interface IRoleRepository
    {
        Task<Role> GetByIdAsync(Guid id);
        // Otros métodos según necesidades...
        Task AddAsync(Role role);
        Task<Role?> GetByNameAsync(string roleName);
        Task SaveChangesAsync();
    }
}
