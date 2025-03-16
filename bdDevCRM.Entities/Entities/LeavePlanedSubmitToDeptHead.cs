using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class LeavePlanedSubmitToDeptHead
{
    public int LeavePlanedSubmitId { get; set; }

    public int? HrRecordId { get; set; }

    public int? FiscalYearId { get; set; }

    public int? IsSentEmailtoDeptHead { get; set; }

    public int? LeaveTypeId { get; set; }
}
