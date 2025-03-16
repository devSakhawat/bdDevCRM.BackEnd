using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class LeaveAdjustmentInfo
{
    public int LeaveAdjustmentId { get; set; }

    public int BalanceId { get; set; }

    public int HrrecordId { get; set; }

    public decimal? OpeningLeaveBalanceOld { get; set; }

    public DateOnly? LeaveYearFrom { get; set; }

    public DateOnly? LeaveYearTo { get; set; }

    public int? LeaveType { get; set; }

    public decimal? LeaveBroughtForwardOld { get; set; }

    public decimal? LeaveEnjoiedOld { get; set; }

    public decimal? ClosingLeaveBalanceOld { get; set; }

    public decimal? LeaveDeductedOld { get; set; }

    public int? FiscalYearId { get; set; }

    public decimal? OpeningLeaveBalanceNew { get; set; }

    public decimal? LeaveBroughtForwardNew { get; set; }

    public decimal? LeaveEnjoiedNew { get; set; }

    public decimal? ClosingLeaveBalanceNew { get; set; }

    public decimal? LeaveDeductedNew { get; set; }

    public int? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    /// <summary>
    /// 1=Insert, 2=Update, 3=Delete
    /// </summary>
    public int? UpdateType { get; set; }

    public string? Remarks { get; set; }
}
