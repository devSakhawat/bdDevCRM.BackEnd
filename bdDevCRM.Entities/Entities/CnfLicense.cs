using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class CnfLicense
{
    public int Id { get; set; }

    public int? AgentId { get; set; }

    public DateTime? IssueDate { get; set; }

    public DateTime? ValiedUpto { get; set; }

    public int? MrNo { get; set; }

    public int? Status { get; set; }

    public int? LiIssuingUserId { get; set; }

    public DateTime? UpdateDate { get; set; }
}
