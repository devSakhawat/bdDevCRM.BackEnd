using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class EarlyExitOutPunchMissApprovedLogForEl
{
    public int EarlyExitOutPunchApprovedLogId { get; set; }

    public int? UserId { get; set; }

    public DateTime? AttendanceDate { get; set; }

    public int? DefalterType { get; set; }
}
