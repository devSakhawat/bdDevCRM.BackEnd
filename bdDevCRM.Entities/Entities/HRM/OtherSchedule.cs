using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class OtherSchedule
{
    public DateTime? OtherScheduleDate { get; set; }

    public string? Remarks { get; set; }

    public int? AddedBy { get; set; }

    public string? AddedDate { get; set; }
}
