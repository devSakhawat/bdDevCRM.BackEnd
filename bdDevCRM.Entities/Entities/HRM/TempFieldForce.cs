using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class TempFieldForce
{
    public string EmployeeId { get; set; } = null!;

    public string? Psocode { get; set; }

    public string? Psolocation { get; set; }

    public string? Rsmcode { get; set; }

    public string? Dsmcode { get; set; }

    public int? UserId { get; set; }

    public string? RegionManagerId { get; set; }

    public string? RegionName { get; set; }

    public DateOnly? EffectiveDate { get; set; }
}
