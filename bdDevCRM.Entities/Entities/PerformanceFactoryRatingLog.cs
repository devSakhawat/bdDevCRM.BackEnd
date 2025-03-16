using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class PerformanceFactoryRatingLog
{
    public int FactoryRatingId { get; set; }

    public int? PerformanceFactoryId { get; set; }

    public int? Rating { get; set; }

    public int? HrrecordId { get; set; }
}
