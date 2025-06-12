using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class AttendanceMonthEndApprovalNotification20231105
{
    public int AttMonthEndAppNotifyId { get; set; }

    public int? HrrecordId { get; set; }

    public DateTime? AttendanceMonth { get; set; }

    public int? NotificationSentStatus { get; set; }
}
