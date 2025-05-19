using System;
using System.Collections.Generic;

namespace Lab_10_Qquelcca.Infrastructure.Context;

public partial class Response
{
    public Guid ResponseId { get; set; }

    public Guid TicketId { get; set; }

    public Guid ResponderId { get; set; }

    public string Message { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public virtual User Responder { get; set; } = null!;

    public virtual Ticket Ticket { get; set; } = null!;
}
