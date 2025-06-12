using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class TempOverTime
{
    public string EmployeeId { get; set; } = null!;

    public DateTime OtfromDate { get; set; }

    public DateTime OttoDate { get; set; }

    public decimal OverTimeHour { get; set; }

    public int UserId { get; set; }

    public int? IsCurrentMonth { get; set; }

    public string? Remarks { get; set; }
}
