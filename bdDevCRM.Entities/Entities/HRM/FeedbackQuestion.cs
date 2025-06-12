using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class FeedbackQuestion
{
    public int FeedbackQuestionId { get; set; }

    public string? QuestionText { get; set; }

    public int? AnswerType { get; set; }

    public int? AnswerStyle { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? Status { get; set; }
}
