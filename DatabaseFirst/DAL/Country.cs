﻿using System;
using System.Collections.Generic;

namespace DatabaseFirst.DAL;

public partial class Country
{
    public int IdCountry { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Trip> Trips { get; set; } = new List<Trip>();
}
