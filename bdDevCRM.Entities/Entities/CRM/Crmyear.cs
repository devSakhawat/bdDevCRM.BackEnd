using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities.CRM;

public partial class Crmyear
{
    public int YearId { get; set; }

    public string YearName { get; set; } = null!;

    public string? YearCode { get; set; }

    public bool? Status { get; set; }
}
