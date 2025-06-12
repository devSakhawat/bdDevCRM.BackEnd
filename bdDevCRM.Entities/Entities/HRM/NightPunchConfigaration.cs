using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class NightPunchConfigaration
{
    public int NightPunchConfigId { get; set; }

    public int CompanyId { get; set; }

    public int BranchId { get; set; }

    public string? PunchTimeFrom { get; set; }

    public string? PunchTimeTo { get; set; }

    public int? IsSameDay { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }
}
