using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class FeedbackAnswered
{
    public int FeedbackAnsweredId { get; set; }

    public int? HrRecordId { get; set; }

    public int? FeedbackId { get; set; }

    public int? FeedbackQuestionId { get; set; }

    public int? QuestionAnswerId { get; set; }

    public DateTime? LastUpdateDate { get; set; }

    public int? FeedbackAnswerStatus { get; set; }

    public int? TrainingScheduleId { get; set; }
}
