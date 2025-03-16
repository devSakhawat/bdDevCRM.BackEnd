using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class VhMasterLocation
{
    public int LocationId { get; set; }

    public string? LocationName { get; set; }

    public int? RegionId { get; set; }

    public int? DivisionId { get; set; }

    public int? IsField { get; set; }

    public int? IsActive { get; set; }
}
