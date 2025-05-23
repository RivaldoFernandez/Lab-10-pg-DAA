using Lab_10_Qquelcca.Domain.Interfaces.Unit;

namespace Lab_10_Qquelcca.Infrastructure.Repositories.Unit;

using Lab_10_Qquelcca.Domain.Interfaces;
using Lab_10_Qquelcca.Domain.Interfaces.Repositories;
using Lab_10_Qquelcca.Infrastructure.Context;
using System.Threading.Tasks;



public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private IUserRepository? _userRepository;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public IUserRepository UserRepository 
        => _userRepository ??= new UserRepository(_context);

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
