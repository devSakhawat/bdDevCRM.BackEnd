using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class AppraisalYearEndProcess
{
    public int AppraisalYearEndProcessId { get; set; }

    public int? HrRecordId { get; set; }

    public int? AppraisalYearId { get; set; }

    public int? CompanyId { get; set; }

    public int? GradeId { get; set; }

    public int? DepartmentId { get; set; }

    public int? DesignationId { get; set; }

    public int? BranchId { get; set; }

    public int? CreateUser { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? UpdateUser { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? Status { get; set; }

    public string? Comments { get; set; }

    public decimal? TotalRatingValue { get; set; }

    public string? RatingStatus { get; set; }

    public int? ApproverId { get; set; }

    public int? DivisionId { get; set; }

    public int? SectionId { get; set; }

    public int? EmploymentTypeId { get; set; }

    public int? PromotedGradeId { get; set; }

    public int? WfstateId { get; set; }

    public bool? IsEligible { get; set; }

    public int? SaveAsDraft { get; set; }
}
