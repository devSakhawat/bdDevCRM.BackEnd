using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class FeedbackQuestionAnswers
{
    public int FeedbackQuestionAnswerId { get; set; }

    public int? QuestionId { get; set; }

    public string? AnswerText { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? SortOrder { get; set; }
}
