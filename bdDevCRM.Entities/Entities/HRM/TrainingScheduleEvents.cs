using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class TrainingScheduleEvents
{
    public int TrainingScheduleCalendarId { get; set; }

    public int TrainingScheduleId { get; set; }

    public DateTime ScheduleCreateDate { get; set; }

    public DateTime ScheduleStartTime { get; set; }

    public DateTime ScheduleEndTime { get; set; }

    public string? RecurrenceRule { get; set; }

    public bool IsAllDay { get; set; }

    public string? Description { get; set; }

    public string? RecurrenceException { get; set; }

    public string? StartTimezone { get; set; }

    public string? EndTimezone { get; set; }

    public string Title { get; set; } = null!;

    public int? Id { get; set; }
}
