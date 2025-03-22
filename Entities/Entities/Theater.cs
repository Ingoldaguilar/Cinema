using System;
using System.Collections.Generic;

namespace Entities.Entities;

public partial class Theater
{
    public int TheaterId { get; set; }

    public string? Name { get; set; }

    public int? TotalSeats { get; set; }

    public virtual ICollection<Seat> Seats { get; set; } = new List<Seat>();

    public virtual ICollection<Showtime> Showtimes { get; set; } = new List<Showtime>();
}
