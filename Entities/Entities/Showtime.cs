using System;
using System.Collections.Generic;

namespace Entities.Entities;

public partial class Showtime
{
    public int ShowtimeId { get; set; }

    public int? MovieId { get; set; }

    public int? TheaterId { get; set; }

    public DateTime? Showtime1 { get; set; }

    public decimal? TicketPrice { get; set; }

    public virtual ICollection<Discount> Discounts { get; set; } = new List<Discount>();

    public virtual Movie? Movie { get; set; }

    public virtual Theater? Theater { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
