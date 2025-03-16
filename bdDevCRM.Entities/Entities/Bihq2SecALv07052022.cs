using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class Bihq2SecALv07052022
{
    public string? EmployeeId { get; set; }

    public DateOnly? LeaveFrom { get; set; }

    public DateOnly? LeaveTo { get; set; }

    public decimal? LeaveDays { get; set; }
}
