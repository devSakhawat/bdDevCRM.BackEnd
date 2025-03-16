using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class QuestionCategory
{
    public int QuestionCategoryId { get; set; }

    public string? QuestionCategoryCode { get; set; }

    public string? QuestionCategoryDescription { get; set; }

    public int? SurveyCategoryId { get; set; }
}
