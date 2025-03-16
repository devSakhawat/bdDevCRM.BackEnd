using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class LeaveBalance2019
{
    public int BalanceId { get; set; }

    public int? HrrecordId { get; set; }

    public decimal? OpeningLeaveBalance { get; set; }

    public DateTime? LeaveYearFrom { get; set; }

    public DateTime? LeaveYearTo { get; set; }

    public int? LeaveType { get; set; }

    public decimal? LeaveBroughtForward { get; set; }

    public decimal? LeaveEnjoied { get; set; }

    public decimal? ClosingLeaveBalance { get; set; }

    public decimal? LeaveDeducted { get; set; }

    public int? FiscalYearId { get; set; }
}
