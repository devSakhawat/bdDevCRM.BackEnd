using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class PayrollCycleSetup
{
    public int PayrollCycleId { get; set; }

    public int PayrollStartDay { get; set; }

    public int? PayrollMonthType { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? IsActive { get; set; }

    public int? IsOnlyAttendance { get; set; }

    public int? MonthendType { get; set; }
}
