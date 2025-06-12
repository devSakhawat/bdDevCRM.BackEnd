using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class LeaveAdjustment
{
    public int LeaveAdjustmentId { get; set; }

    public int? HrrecordId { get; set; }

    public int? DepartmentId { get; set; }

    public int? NoOfLeaveDeduct { get; set; }

    public DateTime? ForTheMonthYear { get; set; }

    public int? LeaveTypeId { get; set; }

    public int? UserId { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? RecommenderId { get; set; }

    public DateTime? RecommandDate { get; set; }

    public string? RecommanderRemarks { get; set; }

    public int? ApproverId { get; set; }

    public DateTime? ApprovedDate { get; set; }

    public int? StatusId { get; set; }

    public decimal? LeaveIncress { get; set; }

    public int? OriginalLeaveDeducted { get; set; }
}
