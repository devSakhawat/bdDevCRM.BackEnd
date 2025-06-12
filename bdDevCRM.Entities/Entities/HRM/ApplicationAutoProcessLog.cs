using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class ApplicationAutoProcessLog
{
    public int AppAutoProcessLogId { get; set; }

    public int? CurrentApproverId { get; set; }

    public int? NextApproverId { get; set; }

    public int? DueDays { get; set; }

    public DateTime? ProcessDate { get; set; }

    public int? ReferenceId { get; set; }

    public DateOnly? ReferenceDate { get; set; }

    public int? ApplicantHrRecordId { get; set; }

    public int? StateId { get; set; }
}
