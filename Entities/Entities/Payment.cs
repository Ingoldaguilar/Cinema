using System;
using System.Collections.Generic;

namespace Entities.Entities;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int? TicketId { get; set; }

    public int? UserId { get; set; }

    public decimal? Amount { get; set; }

    public string? PaymentMethod { get; set; }

    public string? TransactionId { get; set; }

    public DateTime? PaymentDate { get; set; }

    public string? Status { get; set; }

    public virtual Ticket? Ticket { get; set; }

    public virtual User? User { get; set; }
}
