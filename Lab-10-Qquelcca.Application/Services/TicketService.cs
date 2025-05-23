using Lab_10_Qquelcca.Application.DTOs.Response;
using Lab_10_Qquelcca.Application.DTOs.Ticket;
using Lab_10_Qquelcca.Application.Interfaces;
using Lab_10_Qquelcca.Domain.Entities;
using Lab_10_Qquelcca.Domain.Interfaces.Repositories;

namespace Lab_10_Qquelcca.Application.Services;

public class TicketService : ITicketService
{
    private readonly ITicketRepository _ticketRepo;
    private readonly IResponseRepository _responseRepo;

    public TicketService(ITicketRepository ticketRepo, IResponseRepository responseRepo)
    {
        _ticketRepo = ticketRepo;
        _responseRepo = responseRepo;
    }

    public async Task<Guid> CreateTicketAsync(CreateTicketRequest request)
    {
        var ticket = new Ticket()
        {
            TicketId = Guid.NewGuid(),
            UserId = request.UserId,
            Title = request.Title,
            Description = request.Description
        };

        await _ticketRepo.AddAsync(ticket);
        await _ticketRepo.SaveChangesAsync();

        return ticket.TicketId;
    }

    public async Task<bool> AssignTicketAsync(Guid ticketId, Guid technicianId)
    {
        var ticket = await _ticketRepo.GetByIdAsync(ticketId);
        if (ticket == null || ticket.Status != "abierto") return false;

        ticket.Status = "en_proceso";
        ticket.UpdatedAt = DateTime.UtcNow;

        await _ticketRepo.UpdateAsync(ticket);
        await _ticketRepo.SaveChangesAsync();
        return true;
    }

    public async Task<bool> AddResponseAsync(AddResponseRequest request)
    {
        var response = new Response
        {
            ResponseId = Guid.NewGuid(),
            TicketId = request.TicketId,
            ResponderId = request.ResponderId,
            Message = request.Message
        };

        await _responseRepo.AddAsync(response);
        await _responseRepo.SaveChangesAsync();
        return true;
    }

    public async Task<bool> CloseTicketAsync(Guid ticketId)
    {
        var ticket = await _ticketRepo.GetByIdAsync(ticketId);
        if (ticket == null || ticket.Status == "cerrado") return false;

        ticket.Status = "cerrado";
        ticket.ClosedAt = DateTime.UtcNow;
        ticket.UpdatedAt = DateTime.UtcNow;

        await _ticketRepo.UpdateAsync(ticket);
        await _ticketRepo.SaveChangesAsync();
        return true;
    }
}
