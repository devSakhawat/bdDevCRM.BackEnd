using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class OtsettingsBg
{
    public int OverTimeId { get; set; }

    public string? OtPilocyCode { get; set; }

    public string? OtpolicyName { get; set; }

    public int? OttypeId { get; set; }

    public decimal? OtminHour { get; set; }

    public decimal? BreakUpOt { get; set; }

    public int? CompanyId { get; set; }
}
