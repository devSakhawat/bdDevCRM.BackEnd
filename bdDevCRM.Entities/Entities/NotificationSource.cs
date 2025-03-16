using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class NotificationSource
{
    public int NotificationSourceId { get; set; }

    public string? SourceTitle { get; set; }

    public string? BaseUrl { get; set; }
}
