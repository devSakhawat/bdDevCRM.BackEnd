using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class LeaveTemp
{
    public int LeaveTempId { get; set; }

    public string? EmployeeId { get; set; }

    public string? LeaveType { get; set; }

    public double? OpeningLeaveBalance { get; set; }

    public double? LeaveForward { get; set; }

    public double? LeaveEnjoyed { get; set; }

    public double? ForceLeaveDeduction { get; set; }

    public double? ClosingLeaveBalance { get; set; }

    public double? UserId { get; set; }

    public string? IsValid { get; set; }
}
