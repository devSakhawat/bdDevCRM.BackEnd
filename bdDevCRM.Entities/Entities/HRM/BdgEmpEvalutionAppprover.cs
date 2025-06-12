using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class BdgEmpEvalutionAppprover
{
    public int EmpEvalutionAppproverId { get; set; }

    public int? YearId { get; set; }

    public int? HrrecordId { get; set; }

    public int? KpiInfoId { get; set; }

    public int? EvaluatedBy { get; set; }

    public string? Remarks { get; set; }

    public int? IsActive { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? ApproverId { get; set; }

    public int? UpdatedBy { get; set; }

    public decimal? RequestAmount { get; set; }

    public decimal? SpecialAllowance { get; set; }
}
