using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class FeedbackDetails
{
    public int FeedbackDetailsId { get; set; }

    public int? FeedbackId { get; set; }

    public int? QuestionId { get; set; }
}
