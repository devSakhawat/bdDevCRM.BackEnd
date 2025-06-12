using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class EmpManualAttendance
{
    public int EmployeeLogId { get; set; }

    public int? HrRecordId { get; set; }

    public DateTime? Date { get; set; }

    public int? UserId { get; set; }

    public string? Status { get; set; }
}
