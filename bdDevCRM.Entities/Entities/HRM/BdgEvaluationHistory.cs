using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class BdgEvaluationHistory
{
    public int Id { get; set; }

    public int? EmpEvaluationId { get; set; }

    public int? EvaluatedBy { get; set; }

    public DateOnly? EvaluatedDate { get; set; }

    public int? KpiId { get; set; }

    public int? StateId { get; set; }

    public int? PromotedGradeId { get; set; }

    public string? Remarks { get; set; }

    public decimal? RequestAmount { get; set; }

    public decimal? RequestBasicAmount { get; set; }
}
