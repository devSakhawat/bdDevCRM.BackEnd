using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class SurveyCategory
{
    public int SurveyCategoryId { get; set; }

    public string? SurveyCategoryCode { get; set; }

    public string? SurveyCategoryDescription { get; set; }
}
