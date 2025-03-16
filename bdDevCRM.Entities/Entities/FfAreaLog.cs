using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class FfAreaLog
{
    public int AreaLogId { get; set; }

    public string? AreaCode { get; set; }

    public string AreaName { get; set; } = null!;

    public DateTime? CreateDate { get; set; }

    public string? CreateUser { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? UpdateUser { get; set; }

    public int? Status { get; set; }

    public int RegionId { get; set; }

    public int? AreaId { get; set; }
}
