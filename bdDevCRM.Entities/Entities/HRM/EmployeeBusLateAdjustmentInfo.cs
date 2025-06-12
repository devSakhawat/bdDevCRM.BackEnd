using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class EmployeeBusLateAdjustmentInfo
{
    public int EmpBusLateInfoId { get; set; }

    public int? HrRecordId { get; set; }

    public DateOnly? AttendanceDate { get; set; }

    public int? AttendanceLogId { get; set; }

    public int? IsAdjusted { get; set; }
}
