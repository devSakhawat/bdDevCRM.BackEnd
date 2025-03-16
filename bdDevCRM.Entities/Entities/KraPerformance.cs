using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class KraPerformance
{
    public int KraPerformanceId { get; set; }

    public int KraYearId { get; set; }

    public int HrRecordId { get; set; }

    public decimal? TotalWeightPartA { get; set; }

    public decimal? TotalWeightPartB { get; set; }

    public decimal? TotalAchivementA { get; set; }

    public decimal? TotalAchivementB { get; set; }

    public decimal? TotalScore { get; set; }

    public decimal? BeforApproveScore { get; set; }

    public int? StatusId { get; set; }

    public int? ApproverAssignBy { get; set; }

    public int? IsFrezz { get; set; }

    public int? IsMidYearReviewOpen { get; set; }

    public int? IsYearEndReviewOpen { get; set; }

    public int? Approver { get; set; }

    public DateOnly? ApproveDate { get; set; }

    public int? KraCompanyId { get; set; }

    public int? KraLocationId { get; set; }

    public int? KraDepartmentId { get; set; }

    public int? KraDesignationId { get; set; }

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

    public virtual ICollection<KraCompetencies> KraCompetencies { get; set; } = new List<KraCompetencies>();
}
