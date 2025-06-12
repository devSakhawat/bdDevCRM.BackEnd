using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class Sqanswer
{
    public int SqanswerId { get; set; }

    public int SurveyQuestionId { get; set; }

    public string? AnswerText { get; set; }

    public string? SortOrder { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }
}
