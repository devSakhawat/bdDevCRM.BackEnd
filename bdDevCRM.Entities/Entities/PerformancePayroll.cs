using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class PerformancePayroll
{
    public int PerformancePayrollId { get; set; }

    public int? PerformanceReviewId { get; set; }

    public int? NewDivisionId { get; set; }

    public int? NewDepartmentId { get; set; }

    public string? NewLocationId { get; set; }

    public int? NewCompanyId { get; set; }

    public int? HrRecordId { get; set; }

    public int? OldBasic { get; set; }

    public int? GeneralIncrementAmount { get; set; }

    public int? EnhancementAmount { get; set; }

    public int? Gross { get; set; }

    public int NewDesignationId { get; set; }

    public int? NewGradeId { get; set; }

    public int? EvaluationType { get; set; }

    public DateOnly? EffectiveDate { get; set; }

    public DateOnly CreateDate { get; set; }

    public int? CreatedBy { get; set; }

    public DateOnly? UpdateDate { get; set; }

    public int? UpdatedBy { get; set; }

    public int? AdjustmentIncrementAmount { get; set; }

    public int? AdjustmentCount { get; set; }

    public string? Remarks { get; set; }

    public int? CostCenterId { get; set; }
}
