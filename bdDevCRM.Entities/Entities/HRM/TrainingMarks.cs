using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class TrainingMarks
{
    public int TrainingMarkId { get; set; }

    public int? ScheduleId { get; set; }

    public int? UserId { get; set; }

    public int? Status { get; set; }
}
