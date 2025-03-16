using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class FinalSettlement
{
    public int SettlementId { get; set; }

    public int HrRecordId { get; set; }

    public string? EmployeeId { get; set; }

    public string? EmployeeName { get; set; }

    public DateOnly? JoiningDate { get; set; }

    public DateOnly? DiscontinueDate { get; set; }

    public string? GradeName { get; set; }

    public string? DesignationName { get; set; }

    public string? Remarks { get; set; }

    public decimal? LeaveAmount { get; set; }

    public decimal? GratuityAmount { get; set; }

    public decimal? SalaryAmount { get; set; }

    public decimal? SalaryDeduction { get; set; }

    public decimal? LoanDeduction { get; set; }

    public decimal? NetDisbursableAmount { get; set; }

    public decimal? TaxAmount { get; set; }

    public decimal? NetAmount { get; set; }

    public int? Status { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? PaidBy { get; set; }

    public DateTime? PaidDate { get; set; }

    public decimal? PfMemberContribution { get; set; }

    public decimal? PfCompanyContribution { get; set; }

    public decimal? PfMemberProfit { get; set; }

    public decimal? PfCompanyProfit { get; set; }

    public decimal? PfLoanDeduction { get; set; }

    public decimal? SalaryAmountPerDay { get; set; }

    public int? LeaveDays { get; set; }

    public decimal? OverTimeAmount { get; set; }

    public decimal? AuditedProfit { get; set; }

    public decimal? UnAuditedProfit { get; set; }

    public string? SalaryDedRemarks { get; set; }

    public decimal? NoticePayRefund { get; set; }

    public decimal? ExcessLfaamount { get; set; }

    public string? VoucharNo { get; set; }

    public decimal? PfoutStandingTotal { get; set; }

    public decimal? SalaryOutStandingTotal { get; set; }

    public decimal? ActualOwnContribution { get; set; }

    public decimal? ActualCompanyContribution { get; set; }

    public decimal? ActualMemberProfit { get; set; }

    public decimal? ActualCompanyProfit { get; set; }

    public int? IsCompanyContributionDisable { get; set; }

    public decimal? OverTimeHour { get; set; }

    public decimal? ProratedPfcontribution { get; set; }

    public decimal? ProratedPfinterest { get; set; }

    public decimal? Otrate { get; set; }
}
