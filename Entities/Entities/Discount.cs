using System;
using System.Collections.Generic;

namespace Entities.Entities;

public partial class Discount
{
    public int DiscountId { get; set; }

    public string? Code { get; set; }

    public string? Description { get; set; }

    public string? DiscountType { get; set; }

    public decimal? DiscountValue { get; set; }

    public DateTime? ValidFrom { get; set; }

    public DateTime? ValidUntil { get; set; }

    public int? ShowtimeId { get; set; }

    public virtual Showtime? Showtime { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
