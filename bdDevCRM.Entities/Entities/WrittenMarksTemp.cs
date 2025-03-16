using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class WrittenMarksTemp
{
    public int? ApplicantId { get; set; }

    public int? JobVacancyId { get; set; }

    public string? RollNumber { get; set; }

    public decimal? WrittenMarks { get; set; }

    public decimal? EducationMarks { get; set; }
}
