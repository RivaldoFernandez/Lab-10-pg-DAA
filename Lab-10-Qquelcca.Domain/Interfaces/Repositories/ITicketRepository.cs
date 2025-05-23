using Lab_10_Qquelcca.Domain.Entities;


namespace Lab_10_Qquelcca.Domain.Interfaces.Repositories
{
    public interface ITicketRepository
    {
        Task<Ticket> GetByIdAsync(Guid id);
        Task<IEnumerable<Ticket>> GetAllAsync();
        Task AddAsync(Ticket ticket);
        void Update(Ticket ticket);
        void Remove(Ticket ticket);
    }
}