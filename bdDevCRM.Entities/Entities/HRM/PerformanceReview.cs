using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class PerformanceReview
{
    public int PerformanceReviewId { get; set; }

    public string? ReasonForCurrentReview { get; set; }

    public decimal? CurrentBasicSalary { get; set; }

    public decimal? CurrentTakeHomeSalary { get; set; }

    public int? TotalRating { get; set; }

    public int? EvaluationSummery { get; set; }

    public int? CompanyId { get; set; }

    public int? DepartmentId { get; set; }

    public int? RsmregionId { get; set; }

    public int? FieldForceRsmMappingId { get; set; }

    public int? GradeId { get; set; }

    public int? DivisionId { get; set; }

    public string? LengthOfService { get; set; }

    public int? PerformanceAttributesForSupervisor { get; set; }

    public int? PerformanceAttributesForLeadership { get; set; }

    public DateTime? ReviewDate { get; set; }

    public int? SectionId { get; set; }

    public int? FuncId { get; set; }

    public int? StateId { get; set; }

    public DateTime? JoiningDate { get; set; }

    public DateTime? AnticipatedConfirmationDate { get; set; }

    public int? Status { get; set; }

    public int? Designationid { get; set; }

    public int? Branchid { get; set; }

    public int? HrrecordId { get; set; }

    public int? ApproverId { get; set; }

    public string? Crdp { get; set; }

    public string? Comments { get; set; }

    public bool? IsBlock { get; set; }

    public string? EmployeeId { get; set; }

    public int? SupervisorId { get; set; }

    public int? ReviewType { get; set; }

    public int? EmployeeTypeId { get; set; }
}
