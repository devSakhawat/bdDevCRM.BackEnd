using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class InvestmentType
{
    public int InvestmentTypeId { get; set; }

    public string? InvestmentTypeName { get; set; }

    public int? InvestmentAccHeadId { get; set; }

    public int? TrusteeAccountHeadId { get; set; }

    public int? InterestAccountHeadId { get; set; }

    public int? AcruedInterestAccountHeadId { get; set; }

    public int? IsActive { get; set; }
}
