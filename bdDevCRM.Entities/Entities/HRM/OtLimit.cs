using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class OtLimit
{
    public int OtLimitId { get; set; }

    public int? CompanyId { get; set; }

    public int? BranchId { get; set; }

    public int? MonthlyOtLimit { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }
}
