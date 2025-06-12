using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class TempLeaveWithoutPay
{
    public string EmployeeId { get; set; } = null!;

    public decimal LeaveWithoutPay { get; set; }

    public int UserId { get; set; }

    public int? IsBasic { get; set; }
}
