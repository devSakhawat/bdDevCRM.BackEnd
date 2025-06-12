using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class Canteen
{
    public int CanteenId { get; set; }

    public string CanteenCode { get; set; } = null!;

    public string CanteenName { get; set; } = null!;

    public bool IsActive { get; set; }

    public int? CtcId { get; set; }
}
