using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class RegionDivisionMap
{
    public int RegionDivisionMapId { get; set; }

    public int? RegionId { get; set; }

    public int? DivisionId { get; set; }
}
