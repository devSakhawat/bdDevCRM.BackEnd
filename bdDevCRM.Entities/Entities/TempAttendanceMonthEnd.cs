using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class TempAttendanceMonthEnd
{
    public string EmployeeId { get; set; } = null!;

    public int WorkingDays { get; set; }

    public int PresentDays { get; set; }

    public int ApprovedLeave { get; set; }

    public int DayOff { get; set; }

    public int OnsiteClient { get; set; }

    public int AbsentDays { get; set; }

    public int LateDays { get; set; }

    public int ApprovedShortLeave { get; set; }

    public int LeaveDeduction { get; set; }

    public int UserId { get; set; }
}
