using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class BdgBudgetYearCompanyMap
{
    public int BudgetYearCompanyMapId { get; set; }

    public int BudgetYearSettingsId { get; set; }

    public int CompanyId { get; set; }
}
