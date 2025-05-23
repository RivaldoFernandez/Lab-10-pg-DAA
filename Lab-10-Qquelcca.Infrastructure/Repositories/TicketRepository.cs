using Lab_10_Qquelcca.Domain.Entities;
using Lab_10_Qquelcca.Domain.Interfaces.Repositories;
using Lab_10_Qquelcca.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;


namespace Lab_10_Qquelcca.Infrastructure.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly ApplicationDbContext _context;

        public TicketRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Ticket> GetByIdAsync(Guid id)
        {
            return await _context.Tickets.FindAsync(id);
        }

        public async Task<IEnumerable<Ticket>> GetAllAsync()
        {
            return await _context.Tickets.ToListAsync();
        }

        public async Task AddAsync(Ticket ticket)
        {
            await _context.Tickets.AddAsync(ticket);
        }

        public void Update(Ticket ticket)
        {
            _context.Tickets.Update(ticket);
        }

        public void Remove(Ticket ticket)
        {
            _context.Tickets.Remove(ticket);
        }
    }
}