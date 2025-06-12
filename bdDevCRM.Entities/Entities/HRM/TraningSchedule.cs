using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class TraningSchedule
{
    public int TrainingScheduleId { get; set; }

    public int TrainingPlanningId { get; set; }

    public int? TrainingInfoId { get; set; }

    public int? FeedbackId { get; set; }

    public string? Rationale { get; set; }

    public int? IsNominationClose { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int? IsPublished { get; set; }

    public int? IsScheduleComplete { get; set; }

    public int? StateId { get; set; }

    public int? NatureOfTraining { get; set; }

    public string? Place { get; set; }

    public int? ExecutionType { get; set; }

    public int? ConductedBy { get; set; }

    public int? NumberOfParticipent { get; set; }

    public int? MaxNoOfParticipent { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public int? Sbu { get; set; }

    public int? OldScheduleId { get; set; }

    public decimal? TotalCourseFee { get; set; }

    public string? ScheduleStatus { get; set; }

    public DateOnly? CancelDate { get; set; }

    public string? CancelReason { get; set; }
}
