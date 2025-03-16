using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class PfFundEligibleAmountMapping
{
    public int PfFundEligibleAmountMappingId { get; set; }

    public int PfFundEligibleAmountId { get; set; }

    public int AccountHeadId { get; set; }
}
