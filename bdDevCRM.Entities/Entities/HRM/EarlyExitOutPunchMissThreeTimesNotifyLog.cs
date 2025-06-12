using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class EarlyExitOutPunchMissThreeTimesNotifyLog
{
    public int EarlyExitOutPunchId { get; set; }

    public int? HrRecordId { get; set; }

    public DateTime? AdjustmentDate { get; set; }

    public DateTime? TransactionDate { get; set; }
}
