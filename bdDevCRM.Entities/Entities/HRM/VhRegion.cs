using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class VhRegion
{
    public int RegionId { get; set; }

    public string? RegionCode { get; set; }

    public string? RegionName { get; set; }

    public int? IsActive { get; set; }
}
