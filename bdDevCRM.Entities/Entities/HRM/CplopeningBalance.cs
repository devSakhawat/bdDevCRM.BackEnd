using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class CplopeningBalance
{
    public int Id { get; set; }

    public string EmpId { get; set; } = null!;

    public int? OpeningCpl { get; set; }

    public string? Remarks { get; set; }
}
