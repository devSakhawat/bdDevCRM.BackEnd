using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class Feedback
{
    public int FeedbackId { get; set; }

    public string? FeedbackTitle { get; set; }

    public string? Description { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? Status { get; set; }
}
