namespace Lab_10_Qquelcca.Application.DTOs.Ticket;

public class CreateTicketRequest
{
    public Guid UserId { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
}