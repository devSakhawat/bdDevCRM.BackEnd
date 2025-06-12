using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class WorkLog
{
    public int LogId { get; set; }

    public int? HrrecordId { get; set; }

    public DateTime? LogDate { get; set; }

    public DateTime? ApproveDate { get; set; }

    public int? ApproverId { get; set; }

    public int? AssignmentId { get; set; }

    public string? JobDescription { get; set; }

    public int? StateId { get; set; }

    public decimal? WorkHour { get; set; }

    public string? ApproverComment { get; set; }

    public string? MatriceId { get; set; }

    public string? RecomanderComments { get; set; }

    public DateTime? RecomanderDate { get; set; }

    public int? RecomanderId { get; set; }

    public string? RevisionId { get; set; }
}
