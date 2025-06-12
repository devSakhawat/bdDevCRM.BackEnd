using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class CompanyFiscalYearMap
{
    public int CompanyFiscalYearId { get; set; }

    public int CompanyId { get; set; }

    public int FiscalYearId { get; set; }
}
