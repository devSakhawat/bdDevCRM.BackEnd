using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class ReasonOfReview
{
    public int RemarksId { get; set; }

    public int ApplicationId { get; set; }

    public int? ModuleId { get; set; }

    public int? MenuId { get; set; }

    public string? Comments { get; set; }

    public DateTime? RemarksDate { get; set; }

    public int? ApplicantHrRecordId { get; set; }

    public int? ApproverId { get; set; }

    public int? ApproverType { get; set; }

    public int? ApproverSequence { get; set; }

    public int? CurrentStateId { get; set; }

    public int? NextStateId { get; set; }

    public int? ActionOrder { get; set; }

    public string? ActionType { get; set; }
}
