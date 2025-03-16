using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class RecruitmentStandardForm
{
    public int FormId { get; set; }

    public string FormTitle { get; set; } = null!;

    public string? DowloadPath { get; set; }

    public string? FileName { get; set; }

    public string? Extention { get; set; }
}
