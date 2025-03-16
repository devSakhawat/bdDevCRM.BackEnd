using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class Occupation
{
    public int OccupationId { get; set; }

    public string? OccupationName { get; set; }

    public string? OccCode { get; set; }

    public virtual ICollection<Nominee> Nominee { get; set; } = new List<Nominee>();
}
