using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class TrainingHistoryTemp
{
    public int TrainingHistoryTempId { get; set; }

    public string EmployeeId { get; set; } = null!;

    public string? NameofTraining { get; set; }

    public string? NameofInstitution { get; set; }

    public string? Result { get; set; }

    public DateOnly? FromDate { get; set; }

    public DateOnly? ToDate { get; set; }

    public int? UserId { get; set; }
}
