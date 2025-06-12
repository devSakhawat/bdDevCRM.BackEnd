using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class BdgEmpEvalutionHalfYearlyPayroll
{
    public int BdgEmpEvalutionPayrollId { get; set; }

    public int EmpEvaluationId { get; set; }

    public decimal? CurrentGth { get; set; }

    public decimal? CurrentCtc { get; set; }

    public decimal? IncressGth { get; set; }

    public decimal? IncressCtc { get; set; }

    public decimal? NewGth { get; set; }

    public decimal? NewCtc { get; set; }

    public decimal? CurrentBasic { get; set; }

    public decimal? IncressBasic { get; set; }

    public decimal? NewBasic { get; set; }

    public decimal? CurrentGradeBenifit { get; set; }

    public decimal? IncressGradeBenifit { get; set; }

    public decimal? NewGradeBenifit { get; set; }
}
