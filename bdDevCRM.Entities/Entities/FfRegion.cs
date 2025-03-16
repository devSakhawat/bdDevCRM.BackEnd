using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class FfRegion
{
    public int RegionId { get; set; }

    public string? RegionCode { get; set; }

    public string RegionName { get; set; } = null!;

    public DateTime? CreateDate { get; set; }

    public string? CreateUser { get; set; }

    public string? UpdateUser { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? Status { get; set; }

    public int ZoneId { get; set; }

    public int? HaveRegionManager { get; set; }

    public int? RegionManager { get; set; }
}
