using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities.CRM;

public partial class CrmMonth
{
    public int MonthId { get; set; }

    public string CrmmonthName { get; set; } = null!;

    public string? MonthCode { get; set; }

    public bool? Status { get; set; }
}
