using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class EmailContentSourovRec
{
    public int EmailContentId { get; set; }

    public string? EmailSubject { get; set; }

    public string? EmailTitle { get; set; }

    public string? EmailAttachment { get; set; }

    public string? EmailBody { get; set; }

    public string? ImagesPath { get; set; }

    public int? EmailContentStatus { get; set; }

    public int? EmailTitleId { get; set; }

    public int? EmailNotificationId { get; set; }

    public string? Smsbody { get; set; }

    public string? ParamDefination { get; set; }
}
