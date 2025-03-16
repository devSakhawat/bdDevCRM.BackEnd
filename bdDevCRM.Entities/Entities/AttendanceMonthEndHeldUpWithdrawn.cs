using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class AttendanceMonthEndHeldUpWithdrawn
{
    public int AttMonEndHeldUpWithdrawId { get; set; }

    public int? HrRecordId { get; set; }

    public DateTime? AttendanceMonth { get; set; }

    public int? Status { get; set; }
}
