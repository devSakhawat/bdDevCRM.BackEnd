using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class GradeCtcHistory
{
    public int GradeCtcHistoryId { get; set; }

    public int? GradeCtcId { get; set; }

    public int? GradeId { get; set; }

    public int? CtcId { get; set; }

    public decimal? CtcValue { get; set; }

    public int? CurrencyId { get; set; }

    public DateOnly? UpdateDate { get; set; }
}
