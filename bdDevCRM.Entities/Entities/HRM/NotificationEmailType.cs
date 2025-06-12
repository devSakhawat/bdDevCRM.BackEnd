using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class NotificationEmailType
{
    public int EmailNotificationTypeId { get; set; }

    public string? EmailNotificationTypeName { get; set; }

    public int? Status { get; set; }

    public string? ParamDefination { get; set; }

    public int? ModuleId { get; set; }

    public bool? IsDottedLine { get; set; }

    public bool? IsService { get; set; }
}
