using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class LeaveDeductionInformation
{
    public int LeaveDeductionInfoId { get; set; }

    public int HrRecordId { get; set; }

    public int LeaveTypeId { get; set; }

    public decimal DeductedLeave { get; set; }

    public DateTime AttendanceMonth { get; set; }

    public int? ProcessBy { get; set; }

    public DateTime? ProcessDate { get; set; }
}
