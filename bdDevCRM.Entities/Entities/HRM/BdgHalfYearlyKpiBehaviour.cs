using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class BdgHalfYearlyKpiBehaviour
{
    public int HalfYearlyKpiBehaviourId { get; set; }

    public int? BudgetYearId { get; set; }

    public int? ForEmployeeTypeId { get; set; }

    public int? KpiConfigId { get; set; }

    public int? ToBeEmployeeTypeId { get; set; }

    public decimal? Amount { get; set; }

    public decimal? Ot { get; set; }

    public int? IsGradeBenifit { get; set; }
}
