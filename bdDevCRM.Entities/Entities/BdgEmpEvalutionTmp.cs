using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class BdgEmpEvalutionTmp
{
    public int BdgEmpEvalutionTmpId { get; set; }

    public int? YearId { get; set; }

    public string? EmployeeId { get; set; }

    public string? Kpi { get; set; }

    public int? StateId { get; set; }

    public decimal? OtherAllowance { get; set; }
}
