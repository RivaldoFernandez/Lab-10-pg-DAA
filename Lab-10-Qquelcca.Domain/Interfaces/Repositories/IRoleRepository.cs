using Lab_10_Qquelcca.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Lab_10_Qquelcca.Domain.Interfaces.Repositories
{
    public interface IRoleRepository
    {
        Task<Role> GetByIdAsync(Guid id);
        // Otros métodos según necesidades...
    }
}
