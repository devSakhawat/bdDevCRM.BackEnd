using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class DottedLineEmailConfig
{
    public int DottedLineEmailConfigId { get; set; }

    public int? ModuleId { get; set; }

    public int? HrRecordId { get; set; }

    public int? EmailNotificationTypeId { get; set; }

    public bool? IsActive { get; set; }

    public int? SendTypeId { get; set; }
}
