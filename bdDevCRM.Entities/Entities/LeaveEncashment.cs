using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class LeaveEncashment
{
    public int EncashmentId { get; set; }

    /// <summary>
    /// 1=Anual, 2=Casual, 3=Medical, 4=C-Off, 5=Without Pay
    /// </summary>
    public int? LeaveType { get; set; }

    /// <summary>
    /// 1=Encashment, 2=Forwarding
    /// </summary>
    public int? EncashmentType { get; set; }

    public DateTime? AppliedDate { get; set; }

    public int? ApproverId { get; set; }

    public DateTime? ApproveDate { get; set; }

    public int? StateId { get; set; }

    public decimal? AccumulationDays { get; set; }

    public int? HrRecordId { get; set; }

    public DateTime? LeaveYearForm { get; set; }

    public DateTime? LeaveYearTo { get; set; }

    public int? PaidBy { get; set; }

    public DateTime? PaidDate { get; set; }

    public int? PaySlipNo { get; set; }

    public int? EncashmentAmount { get; set; }

    public int? LeaveTypeId { get; set; }

    public int? FiscalYearId { get; set; }

    public decimal? NormalLeaveDays { get; set; }

    public decimal? LeaveBroughtForward { get; set; }

    public decimal? EncashmentDays { get; set; }

    public decimal? YearEndBalance { get; set; }

    public decimal? LeaveAvaill { get; set; }

    public decimal? NextYearCarryForward { get; set; }

    public decimal? TotalLeaveBalanceForNextYear { get; set; }

    public int? IsApprove { get; set; }

    public int? ApproveBy { get; set; }

    public int? CompanyId { get; set; }

    public int? BranchId { get; set; }

    public int? DivisionId { get; set; }

    public int? DepartmentId { get; set; }

    public int? SectionId { get; set; }

    public int? FacilityId { get; set; }

    public int? GradeId { get; set; }

    public int? DesignationId { get; set; }

    public int? EmployeeTypeId { get; set; }

    public int? PrevLeaveEncashmentDays { get; set; }

    public decimal? PrevLeaveEncashmentAmt { get; set; }

    public decimal? EncashmentAmountPerDay { get; set; }

    public decimal? CurrentBasic { get; set; }

    public int? GenerateBy { get; set; }

    public DateTime? GenerateDate { get; set; }

    public int? PaymentBy { get; set; }

    public DateTime? PaymentDate { get; set; }

    public int? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? IsPaid { get; set; }
}
