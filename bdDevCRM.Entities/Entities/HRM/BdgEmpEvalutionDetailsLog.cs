using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class BdgEmpEvalutionDetailsLog
{
    public int BdgEmpEvalutionDetailsLogId { get; set; }

    public int? BdgEmpEvalutionDetailsId { get; set; }

    public int? EmpEvaluationId { get; set; }

    public int? CtcId { get; set; }

    public decimal? PresentValue { get; set; }

    public decimal? IncrementedValue { get; set; }

    public decimal? NewCtcValue { get; set; }

    public decimal? IncrimentPersentage { get; set; }
}
