using Lab_10_Qquelcca.Domain.Entities;

namespace Lab_10_Qquelcca.Domain.Interfaces.Repositories
{
    public interface IResponseRepository
    {
        Task<Response> GetByIdAsync(Guid id);
        Task<IEnumerable<Response>> GetAllAsync();
        Task AddAsync(Response response);
        void Update(Response response);
        void Remove(Response response); 
        Task SaveChangesAsync();
    }
}