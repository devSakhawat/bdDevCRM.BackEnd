using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class TrainingPlanning
{
    public int TrainingPlanningId { get; set; }

    public int? TrainingInfoId { get; set; }

    public int? TrainingVenueId { get; set; }

    public int? CoOrdinatedBy { get; set; }

    public DateOnly? TrainingFromDate { get; set; }

    public DateOnly? TrainingToDate { get; set; }

    public int? TrainingDays { get; set; }

    public int? MaxParticipant { get; set; }

    public decimal? TotalCost { get; set; }

    public decimal? Duration { get; set; }

    public int? IsPlanCompleted { get; set; }

    public int? IsScheduled { get; set; }

    public int? AddedBy { get; set; }

    public DateTime? AddedDate { get; set; }

    public int? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? OldCoOrdinator { get; set; }

    public string? ChangeReason { get; set; }
}
