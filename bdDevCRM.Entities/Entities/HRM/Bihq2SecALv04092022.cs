using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class Bihq2SecALv04092022
{
    public string? EmployeeId { get; set; }

    public string? Name { get; set; }

    public DateOnly? LeaveFrom { get; set; }

    public DateOnly? LeaveTo { get; set; }

    public string? LeaveTypeCode { get; set; }

    public decimal? LeaveDays { get; set; }
}
