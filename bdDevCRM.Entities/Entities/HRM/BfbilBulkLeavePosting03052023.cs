using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class BfbilBulkLeavePosting03052023
{
    public string? EmployeeId { get; set; }

    public DateOnly? LeaveDate { get; set; }

    public string? LeaveTypeCode { get; set; }
}
