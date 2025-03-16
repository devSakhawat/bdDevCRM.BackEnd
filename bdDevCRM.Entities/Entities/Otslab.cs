using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class Otslab
{
    public int OtslabId { get; set; }

    public int? OverTimeId { get; set; }

    public int? SortOrder { get; set; }

    public decimal? OtfromMin { get; set; }

    public decimal? OttoMin { get; set; }

    public decimal? CalculateMin { get; set; }
}
