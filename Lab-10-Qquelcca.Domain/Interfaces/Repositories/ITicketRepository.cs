using Lab_10_Qquelcca.Domain.Entities;


namespace Lab_10_Qquelcca.Domain.Interfaces.Repositories
{
    public interface ITicketRepository
    {
        Task<Ticket?> GetByIdAsync(Guid ticketId);
        Task AddAsync(Ticket ticket);
        Task UpdateAsync(Ticket ticket);
        Task SaveChangesAsync();
        Task<IEnumerable<Ticket>> GetAllAsync();
    }
}