using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class InterviewMarking
{
    public int MarkingId { get; set; }

    public int? Rating { get; set; }

    public string? MarkingTitle { get; set; }
}
