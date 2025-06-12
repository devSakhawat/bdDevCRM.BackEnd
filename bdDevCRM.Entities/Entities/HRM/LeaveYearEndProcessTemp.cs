using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class LeaveYearEndProcessTemp
{
    public int? HrRecordId { get; set; }

    public int? LeaveTypeId { get; set; }

    public int? FiscalYearId { get; set; }

    public decimal? NormalLeaveDays { get; set; }

    public decimal? LeaveBroughtForward { get; set; }

    public decimal? EncashmentDays { get; set; }

    public decimal? EncashmentAmount { get; set; }

    public decimal? YearEndBalance { get; set; }

    public decimal? LeaveAvaill { get; set; }

    public decimal? NextYearCarryForward { get; set; }

    public decimal? TotalLeaveBalanceForNextYear { get; set; }

    public decimal? PreviousYearOpeningBalance { get; set; }
}
