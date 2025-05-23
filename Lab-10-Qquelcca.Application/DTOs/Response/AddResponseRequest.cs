namespace Lab_10_Qquelcca.Application.DTOs.Response;

public class AddResponseRequest
{
    public Guid TicketId { get; set; }
    public Guid ResponderId { get; set; }
    public string Message { get; set; } = null!;
}