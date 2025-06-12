using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class SurveyQuestion
{
    public int SurveyQuestionId { get; set; }

    public int QuestionCategoryId { get; set; }

    public string? QuestionText { get; set; }

    /// <summary>
    /// 1=Single,2=Multiple
    /// </summary>
    public int? AnswerType { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? SurveyCategoryId { get; set; }

    public int? AnswerStyle { get; set; }

    public int? SortOrder { get; set; }
}
