using Lab_10_Qquelcca.Application.DTOs.Response;
using Lab_10_Qquelcca.Application.DTOs.Ticket;

namespace Lab_10_Qquelcca.Application.Interfaces;

public interface ITicketService
{
    Task<Guid> CreateTicketAsync(CreateTicketRequest request);
    Task<bool> AssignTicketAsync(Guid ticketId, Guid technicianId);
    Task<bool> AddResponseAsync(AddResponseRequest request);
    Task<bool> CloseTicketAsync(Guid ticketId);
}
