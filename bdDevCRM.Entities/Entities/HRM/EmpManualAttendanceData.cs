using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class EmpManualAttendanceData
{
    public int ManualAttendanceId { get; set; }

    public DateOnly? ManualAttendanDate { get; set; }

    public int? HrrecordId { get; set; }

    public string? IsPresent { get; set; }

    public int? IsApproved { get; set; }

    public int? CompanyId { get; set; }

    public int? BranchId { get; set; }

    public int? InsertBy { get; set; }

    public DateTime? InsertDate { get; set; }

    public int? ApprovedBy { get; set; }

    public DateTime? ApprovedDate { get; set; }
}
