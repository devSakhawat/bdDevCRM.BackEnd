using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class NotificationException
{
    public int NotificationExceptionId { get; set; }

    public int? NotificationTypeId { get; set; }

    public int? HrRecordId { get; set; }
}
