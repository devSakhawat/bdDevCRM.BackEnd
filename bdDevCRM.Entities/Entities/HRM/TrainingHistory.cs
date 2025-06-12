using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class TrainingHistory
{
    public int TrainingHistoryId { get; set; }

    public int HrrecordId { get; set; }

    public string? NameofTraining { get; set; }

    public DateOnly? Period { get; set; }

    public string? NameofInstitution { get; set; }

    public string? Result { get; set; }

    public DateOnly? PeriodTo { get; set; }
}
