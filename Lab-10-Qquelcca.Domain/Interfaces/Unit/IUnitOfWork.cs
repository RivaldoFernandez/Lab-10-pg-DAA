using Lab_10_Qquelcca.Domain.Interfaces.Repositories;
using System.Threading.Tasks;

namespace Lab_10_Qquelcca.Domain.Interfaces.Unit;

public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }
    Task<int> SaveAsync();
}