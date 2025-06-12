using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class LeavePolicyDetails
{
    public int LeavePolicyDetailsId { get; set; }

    public int LeavePolicyId { get; set; }

    public int? EmployeeTypeId { get; set; }

    /// <summary>
    /// 1=Male;2=Female; 3 = both
    /// </summary>
    public int? Gender { get; set; }

    public int? AllocatedDays { get; set; }

    public int? MaxEncash { get; set; }

    public int? MaxForward { get; set; }

    public int? MaxBalance { get; set; }

    public int? MaxConsicativeDays { get; set; }

    public int? MaxMonthlyDays { get; set; }

    public int? ApplicationBeforeDays { get; set; }

    public int? MinConsicativeDays { get; set; }
}
