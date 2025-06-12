using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class OverTimeApprovalHistory
{
    public int OverTimeApprovalHistoryId { get; set; }

    public int? HrRecordId { get; set; }

    public DateOnly? OverTimeMonth { get; set; }

    public int? StateId { get; set; }

    public int? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public DateOnly? OverTimeFromDate { get; set; }

    public DateOnly? OverTimeToDate { get; set; }
}
