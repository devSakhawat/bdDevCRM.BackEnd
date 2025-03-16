using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class VigilanceDutyTime
{
    public int Id { get; set; }

    public int? VigilanceRosterId { get; set; }

    public int? HrRecordId { get; set; }

    public DateTime? VigilanceDate { get; set; }

    public DateTime? ToDate { get; set; }

    public TimeOnly? StartTime { get; set; }

    public TimeOnly? ToTime { get; set; }

    public double? AverageVigilanceTime { get; set; }

    public string RewardType { get; set; } = null!;

    public DateOnly? RewardDate { get; set; }

    public byte? LeaveDayCount { get; set; }

    public TimeOnly? GraceTime { get; set; }

    public string? Remarks { get; set; }

    public DateTime? ActualLoginTime { get; set; }

    public DateTime? ActualLogoutTime { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? DutyStarts { get; set; }

    public bool? IsProcessed { get; set; }

    public double? VigilanceDoneHour { get; set; }

    public bool? IsNightShift { get; set; }
}
