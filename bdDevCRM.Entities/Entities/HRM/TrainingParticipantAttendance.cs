using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class TrainingParticipantAttendance
{
    public int ParticipantAttendanceId { get; set; }

    public int HrRecordId { get; set; }

    public DateOnly AttendanceDate { get; set; }

    public string IsPresent { get; set; } = null!;

    public DateOnly? TrainingFromDate { get; set; }

    public DateOnly? TrainingToDate { get; set; }

    public int TrainingScheduleId { get; set; }

    public int TrainingPlanningId { get; set; }

    public int TrainingInfoId { get; set; }

    public int? AddedBy { get; set; }

    public DateTime? AddedDate { get; set; }
}
