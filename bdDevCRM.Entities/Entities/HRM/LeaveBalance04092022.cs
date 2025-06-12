using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class LeaveBalance04092022
{
    public int BalanceId { get; set; }

    public int? HrrecordId { get; set; }

    public decimal? OpeningLeaveBalance { get; set; }

    public DateOnly? LeaveYearFrom { get; set; }

    public DateOnly? LeaveYearTo { get; set; }

    public int? LeaveType { get; set; }

    public decimal? LeaveBroughtForward { get; set; }

    public decimal? LeaveEnjoied { get; set; }

    public decimal? ClosingLeaveBalance { get; set; }

    public decimal? LeaveDeducted { get; set; }

    public int? FiscalYearId { get; set; }

    public string? Remarks { get; set; }
}
