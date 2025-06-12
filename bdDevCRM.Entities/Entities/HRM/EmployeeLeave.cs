using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class EmployeeLeave
{
    public int EmployeeLeaveId { get; set; }

    public int? HrRecordId { get; set; }

    public int? FiscalYearId { get; set; }

    public int? LeaveTypeId { get; set; }

    public int? AllocatedDays { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? CreateBy { get; set; }

    public int? UpdateBy { get; set; }
}
