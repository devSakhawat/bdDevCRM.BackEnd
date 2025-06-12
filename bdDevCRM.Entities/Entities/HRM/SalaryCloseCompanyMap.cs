using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class SalaryCloseCompanyMap
{
    public int SalaryCloseCompanyMapId { get; set; }

    public int? SalaryCloseId { get; set; }

    public int? CompanyId { get; set; }
}
