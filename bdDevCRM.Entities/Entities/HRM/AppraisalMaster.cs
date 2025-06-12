using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class AppraisalMaster
{
    public int AppraisalMasterId { get; set; }

    public int? HrRecordId { get; set; }

    public int? AppraisalYearId { get; set; }

    public int? AppraisalMonthId { get; set; }

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

    public int? SaveAsDraft { get; set; }

    public int? AppraisalYear { get; set; }
}
