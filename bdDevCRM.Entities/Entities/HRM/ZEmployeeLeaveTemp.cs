using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class ZEmployeeLeaveTemp
{
    public string? EmployeeId { get; set; }

    public string? LeaveType { get; set; }

    public string? OpeningLeaveBalance { get; set; }

    public string? LeaveForward { get; set; }

    public string? LeaveEnjoyed { get; set; }

    public string? ForceLeaveDeducted { get; set; }

    public string? ClosingLeaveBalance { get; set; }
}
