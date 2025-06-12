using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class Otsettings
{
    public int OverTimeId { get; set; }

    public int? OttypeId { get; set; }

    public string? OtstartHour { get; set; }

    public decimal? OtminHour { get; set; }

    public decimal? OtmaxHour { get; set; }

    public decimal? OttotalHour { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? ChangedBy { get; set; }

    public int? Status { get; set; }
}
