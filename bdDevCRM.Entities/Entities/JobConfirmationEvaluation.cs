using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class JobConfirmationEvaluation
{
    public int JobConfirmationEvaluateId { get; set; }

    public int? JobConfirmationMasterId { get; set; }

    public int? HrRecordId { get; set; }

    public int? RecommendationType { get; set; }

    public int? GradeId { get; set; }

    public int? DesignationId { get; set; }

    public int? ExtendedMonth { get; set; }

    public DateTime? EffectiveFrom { get; set; }

    public DateTime? EffectiveFromMto { get; set; }

    public int? IsTerminate { get; set; }

    public int? EvaluateBy { get; set; }

    public int? EmployeeType { get; set; }

    public int? EmpDesignationId { get; set; }

    public int? DesignationIdNonMng { get; set; }

    public DateTime? EffectiveFromNonMng { get; set; }

    public decimal? GrossPay { get; set; }

    public decimal? CostToCompany { get; set; }

    public int? FinalApprovedBy { get; set; }

    public DateTime? EvaluationDate { get; set; }

    public DateTime? FinalApprovedDate { get; set; }
}
