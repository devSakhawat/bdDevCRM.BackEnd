using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class EducationTemp
{
    public int EducationTempId { get; set; }

    public string? EmployeeId { get; set; }

    public string? Certificate { get; set; }

    public string? Institute { get; set; }

    public string? Yearofcompletion { get; set; }

    public string? Result { get; set; }

    public string? Board { get; set; }

    public string? ResultStatus { get; set; }

    public string? HighestEducation { get; set; }

    public int? UserId { get; set; }
}
