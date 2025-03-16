using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class AttendanceFlag
{
    public int AttendanceFlagId { get; set; }

    public string? Description { get; set; }

    public string? Code { get; set; }

    public string? KeyValue { get; set; }
}
