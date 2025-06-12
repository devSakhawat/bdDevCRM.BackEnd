using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class TrainingMarksTemp
{
    public int TrainingMarksId { get; set; }

    public string? EmployeeId { get; set; }

    public decimal? Marks { get; set; }

    public int? TrainingScheduleId { get; set; }
}
