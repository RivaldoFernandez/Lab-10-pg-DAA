using System;
using System.Collections.Generic;

namespace Lab_10_Qquelcca.Infrastructure.Context;

public partial class Ticket
{
    public Guid TicketId { get; set; }

    public Guid UserId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string Status { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? ClosedAt { get; set; }

    public virtual ICollection<Response> Responses { get; set; } = new List<Response>();

    public virtual User User { get; set; } = null!;
}
