using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class FfTerritoryLog
{
    public int TerritoryLogId { get; set; }

    public string? TerritoryCode { get; set; }

    public string TerritoryName { get; set; } = null!;

    public DateTime? CreateDate { get; set; }

    public string? CreateUser { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? UpdateUser { get; set; }

    public int? Status { get; set; }

    public int? TerritoryId { get; set; }

    public int AreaId { get; set; }
}
