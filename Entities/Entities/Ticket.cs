using System;
using System.Collections.Generic;

namespace Entities.Entities;

public partial class Ticket
{
    public int TicketId { get; set; }

    public int? UserId { get; set; }

    public int? ShowtimeId { get; set; }

    public int? SeatId { get; set; }

    public DateTime? PurchaseDate { get; set; }

    public decimal? Price { get; set; }

    public string? PaymentStatus { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Seat? Seat { get; set; }

    public virtual Showtime? Showtime { get; set; }

    public virtual User? User { get; set; }

    public virtual ICollection<Discount> Discounts { get; set; } = new List<Discount>();
}
