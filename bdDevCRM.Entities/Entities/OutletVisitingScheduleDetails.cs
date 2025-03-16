using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class OutletVisitingScheduleDetails
{
    public int OutletVisitingScheduleDetailsId { get; set; }

    public int OutletVisitingScheduleId { get; set; }

    public int OutletId { get; set; }

    public string? Title { get; set; }

    public DateOnly VisitingDate { get; set; }

    public DateTime VisitStartDateTime { get; set; }

    public DateTime VisitEndDateTime { get; set; }

    public bool? IsAllDay { get; set; }

    public string? Description { get; set; }

    public string? StartTimezone { get; set; }

    public string? EndTimezone { get; set; }

    public string? RecurrenceRule { get; set; }

    public string? RecurrenceException { get; set; }

    public int? Status { get; set; }
}
