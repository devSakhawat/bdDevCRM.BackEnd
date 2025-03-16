using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class LoanInformationAudit
{
    public int LoanInformationAuditId { get; set; }

    public int? LoanId { get; set; }

    public int HrRecordId { get; set; }

    public int LoanTypeId { get; set; }

    public decimal LoanAmount { get; set; }

    public int LoanEmi { get; set; }

    public int EmiType { get; set; }

    public decimal? ConsideredLoanAmount { get; set; }

    public int? ConsideredLoanEmi { get; set; }

    public decimal? ConsideredInterestRate { get; set; }

    public decimal? InstalmentAmount { get; set; }

    public decimal? TotalPaid { get; set; }

    public decimal? DueAmount { get; set; }

    public int? StatusId { get; set; }

    public int? ScheduleGenerateBy { get; set; }

    public DateTime? ScheduleDate { get; set; }

    public int? ApproverId { get; set; }

    public DateTime? ApproveDate { get; set; }

    public int? DisburshedId { get; set; }

    public DateTime? DisburshmentDate { get; set; }

    public DateTime? RecoveryStartDate { get; set; }

    public int? RejectedBy { get; set; }

    public DateTime? RejectDate { get; set; }

    public string? Remarks { get; set; }

    public string? Reason { get; set; }

    public bool? LoanPurposeIsList { get; set; }

    public string? Recomendation { get; set; }

    public string? ApproverComments { get; set; }

    public string? VoucherNo { get; set; }

    public DateOnly? ApplyDate { get; set; }

    public int? CompanyId { get; set; }

    public int? BranchId { get; set; }

    public int? DivisionId { get; set; }

    public int? DepartmentId { get; set; }

    public int? FacilityId { get; set; }

    public int? SectionId { get; set; }

    public int? DesignationId { get; set; }

    public int? GradeId { get; set; }

    public int? EmployeeTypeId { get; set; }

    public int? FuncId { get; set; }

    public int? DownpaymentAmount { get; set; }

    public int? DummyInstalmentAmount { get; set; }

    public DateTime? LastUpdateDate { get; set; }

    public int? UpdateBy { get; set; }
}
