using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class HrTest
{
    public string? EmployeeId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string? LeaveTypeName { get; set; }
}
