using System;
using System.Collections.Generic;

namespace Entities.Entities;

public partial class Session
{
    public Guid SessionId { get; set; }

    public int? UserId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? ExpiresAt { get; set; }

    public virtual User? User { get; set; }
}
