using System;
using System.Collections.Generic;

namespace Entities.Entities;

public partial class Seat
{
    public int SeatId { get; set; }

    public int? TheaterId { get; set; }

    public string? SeatNumber { get; set; }

    public string? SeatType { get; set; }

    public decimal? PriceMultiplier { get; set; }

    public bool? IsAvailable { get; set; }

    public virtual Theater? Theater { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
