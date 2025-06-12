using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class OnSiteClientConveyance
{
    public int OnsiteConveyanceId { get; set; }

    public int? OnsiteClientId { get; set; }

    public DateTime? AppliedDate { get; set; }

    public decimal? ApplicantAmount { get; set; }

    public decimal? RecommandedAmount { get; set; }

    public decimal? ApprovedAmount { get; set; }

    public string? TransportDescription { get; set; }

    public string? RecomaderRemarks { get; set; }

    public string? ApproverRemarks { get; set; }
}
