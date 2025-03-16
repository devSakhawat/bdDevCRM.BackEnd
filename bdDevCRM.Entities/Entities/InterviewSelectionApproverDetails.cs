using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class InterviewSelectionApproverDetails
{
    public int SelectionRemarksId { get; set; }

    public int? SelectionApplicantId { get; set; }

    public string? SelectionComments { get; set; }

    public DateTime? SelectionApprovedDate { get; set; }

    public int? SelectionApprovedBy { get; set; }

    public int? InterviewType { get; set; }

    public int? SelectionSequence { get; set; }

    public int SelectionMenuId { get; set; }

    public int? SelectionModuleId { get; set; }

    public int? SelectionAssignApproverId { get; set; }

    public int? JobVacancyId { get; set; }
}
