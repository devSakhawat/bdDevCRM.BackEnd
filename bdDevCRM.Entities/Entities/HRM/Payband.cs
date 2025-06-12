using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class Payband
{
    public int PaybandId { get; set; }

    public string PaybandName { get; set; } = null!;

    public int? PaybandType { get; set; }

    public int? LeftPercent { get; set; }

    public int? MidlePercent { get; set; }

    public int? RightPercent { get; set; }

    public int? IsActive { get; set; }
}
