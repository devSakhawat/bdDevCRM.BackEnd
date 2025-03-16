using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class LeavePlaned
{
    public int LeavePlaneId { get; set; }

    public int HrRecordId { get; set; }

    public int LeaveTypeId { get; set; }

    public DateTime PlaneDate { get; set; }

    public int Status { get; set; }

    public int? FiscalYearId { get; set; }

    public int? IsApproved { get; set; }

    public int? IsDrafted { get; set; }

    public DateTime? AppliedDate { get; set; }

    public DateTime? ApprovedDate { get; set; }

    public int? IsLeavePosting { get; set; }
}
