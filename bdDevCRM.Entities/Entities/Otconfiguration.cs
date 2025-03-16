using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class Otconfiguration
{
    public int OtconfigurationId { get; set; }

    public int GradeId { get; set; }

    public decimal? OtminHour { get; set; }

    public decimal? OtmaxHour { get; set; }

    public decimal? Otrate { get; set; }

    public int? IsPercentage { get; set; }

    public DateTime? OtconfigureDate { get; set; }

    public int? ChangedBy { get; set; }

    public int? Status { get; set; }

    public int? SalaryHead { get; set; }
}
