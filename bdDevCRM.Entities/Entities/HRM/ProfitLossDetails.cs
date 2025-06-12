using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class ProfitLossDetails
{
    public int ProfitLossDetailsId { get; set; }

    public int ProfitLossMasterId { get; set; }

    public int? AccountHeadId { get; set; }

    public decimal? IncomeAmt { get; set; }

    public decimal? ExpenseAmt { get; set; }
}
