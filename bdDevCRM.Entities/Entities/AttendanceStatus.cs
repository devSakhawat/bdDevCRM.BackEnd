using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class AttendanceStatus
{
    public int AttendanceStatusSettingsId { get; set; }

    public int? IsManualAttendanceStatusActive { get; set; }

    public int? IsAvailableTextRplacement { get; set; }

    public string? NewAvailableText { get; set; }

    public int? IsDayOffTextRplacement { get; set; }

    public string? NewDayOffText { get; set; }

    public int? IsAbsentTextRplacement { get; set; }

    public string? NewAbsentText { get; set; }
}
