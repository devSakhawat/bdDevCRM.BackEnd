using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class BdgEmpEvalutionHalfYearly
{
    public int EmpEvaluationId { get; set; }

    public string? IsReviewed { get; set; }

    public int? YearId { get; set; }

    public int? HrrecordId { get; set; }

    public int? KpiInfoId { get; set; }

    public int? IsActive { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? CreatedBy { get; set; }

    public string? Remarks { get; set; }

    public decimal? RequestAmount { get; set; }

    public int? StateId { get; set; }

    public int? CompanyId { get; set; }

    public int? DesignationId { get; set; }

    public int? GradeId { get; set; }

    public int? PromotedGradeId { get; set; }

    public int? DivisionId { get; set; }

    public int? FacilityId { get; set; }

    public int? BranchId { get; set; }

    public int? SectionId { get; set; }

    public int? EmployeeTypeId { get; set; }

    public int? FunctionId { get; set; }

    public int? RsmRegionId { get; set; }

    public int? DepartmentId { get; set; }
}
