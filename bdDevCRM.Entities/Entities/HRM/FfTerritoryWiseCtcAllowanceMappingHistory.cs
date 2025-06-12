using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class FfTerritoryWiseCtcAllowanceMappingHistory
{
    public int TerritoryWiseAllowanceMappingHistoryId { get; set; }

    public int? TerritoryId { get; set; }

    public int? CtcId { get; set; }

    public decimal? AllowanceAmount { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? CreateUser { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? UpdateUser { get; set; }

    public int? RegionId { get; set; }

    public int? AreaId { get; set; }
}
