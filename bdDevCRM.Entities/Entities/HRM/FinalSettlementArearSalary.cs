using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class FinalSettlementArearSalary
{
    public int FinalSettlementArearSalaryId { get; set; }

    public int SettlementId { get; set; }

    public int CtcId { get; set; }

    public decimal? CtcValue { get; set; }
}
