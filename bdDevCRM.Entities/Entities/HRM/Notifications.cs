using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class Notifications
{
    public int NotificationId { get; set; }

    public string? NotificationTitle { get; set; }

    public string? NotificationDetails { get; set; }

    public DateTime? NotificationDate { get; set; }

    public int? HrRecordId { get; set; }

    public int? NotificationSourceId { get; set; }

    public string? NotificationRedirect { get; set; }

    public bool? IsRead { get; set; }

    public DateTime? ReadTime { get; set; }
}
