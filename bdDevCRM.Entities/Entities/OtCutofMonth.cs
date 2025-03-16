using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class OtCutofMonth
{
    public int OtCutOfMonthId { get; set; }

    public int? CompanyId { get; set; }

    public int? BranchId { get; set; }

    public DateTime? SalaryMonth { get; set; }

    public DateTime? OtFromDate { get; set; }

    public DateTime? OtToDate { get; set; }

    public int? IsActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateBy { get; set; }
}
