using System;
using System.Collections.Generic;

namespace DatabaseFirst.DAL;

public partial class ClientTrip
{
    public int IdClient { get; set; }

    public int IdTrip { get; set; }

    public DateTime RegisteredAt { get; set; }

    public DateTime? PaymentDate { get; set; }

    public virtual Client ClientNavigation { get; set; } = null!;

    public virtual Trip TripNavigation { get; set; } = null!;
}
