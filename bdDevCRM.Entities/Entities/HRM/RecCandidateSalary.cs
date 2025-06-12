using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class RecCandidateSalary
{
    public int SalaryInfoId { get; set; }

    public int? ApplicantId { get; set; }

    public int? CtcId { get; set; }

    public decimal? CtcAmount { get; set; }
}
