using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class OverTimeMonthEndRecommendedNotification
{
    public int OtmonthEndRecNotifyId { get; set; }

    public int? HrrecordId { get; set; }

    public DateTime? OverTimeMonth { get; set; }

    public int? NotificationSentStatus { get; set; }
}
