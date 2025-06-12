using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class Idpolicy
{
    public int IdNo { get; set; }

    public string EntityName { get; set; } = null!;

    public string Prefix { get; set; } = null!;

    public int StartNumber { get; set; }

    public int NumberDigit { get; set; }

    public int? LastNumber { get; set; }

    public string? Suffix { get; set; }

    public int? YearName { get; set; }

    public int? MonthName { get; set; }

    public int? DateName { get; set; }
}
