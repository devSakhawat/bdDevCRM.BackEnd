using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class BdgBudgetYearConfig
{
    public int BudgetYearSettingsId { get; set; }

    public int? BudgetYear { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public DateTime? BudgetStartDate { get; set; }

    public DateTime? PayrollEffectiveDate { get; set; }

    public decimal? MinimumAllownce { get; set; }

    public decimal? MaximumAllownce { get; set; }

    public int? IsActive { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public int? IsActiveYear { get; set; }

    public string? BudgetYearName { get; set; }

    public int? IsApplytoContractualDl { get; set; }
}
