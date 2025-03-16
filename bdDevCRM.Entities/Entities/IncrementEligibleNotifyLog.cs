using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class IncrementEligibleNotifyLog
{
    public int IncrementEligibleNotifyLogId { get; set; }

    public int? HrRecordId { get; set; }

    public DateOnly? EffectiveDate { get; set; }

    public decimal? EmployeeCurrentBasic { get; set; }

    public decimal? BasicAfterIncrement { get; set; }

    public DateOnly? NotifyDate { get; set; }
}
