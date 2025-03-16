using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class FeedbackForEmployee
{
    public int? FeedbackId { get; set; }

    public int? HrRecordId { get; set; }

    public int? Status { get; set; }

    public int? QuestionId { get; set; }

    public string? QuestionText { get; set; }

    public int? AnswerType { get; set; }

    public int? AnswerStyle { get; set; }

    public string? AnswerText { get; set; }

    public int? FeedbackQuestionAnswerId { get; set; }

    public int? SortOrder { get; set; }

    public int FeedbackForEmployeeId { get; set; }
}
