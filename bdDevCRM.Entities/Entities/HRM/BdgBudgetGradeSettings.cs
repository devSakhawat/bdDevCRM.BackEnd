using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class BdgBudgetGradeSettings
{
    public int BudgetGradeSettingsId { get; set; }

    public int? CtcId { get; set; }

    public int? GradeId { get; set; }

    public decimal? Amount { get; set; }

    public int? BudgetYearId { get; set; }

    public int? IsHistory { get; set; }

    public int? IsActive { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }
}
