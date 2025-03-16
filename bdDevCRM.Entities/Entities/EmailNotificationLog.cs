using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class EmailNotificationLog
{
    public int EmailNotificationLogId { get; set; }

    public int? EmailNotificationConfigId { get; set; }

    public DateTime? GeneratedTime { get; set; }

    public int? SentEmailId { get; set; }
}
