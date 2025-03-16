using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class PfFundEligibleAmount
{
    public int PfFundEligibleAmountId { get; set; }

    public string PfFundEligibleAmountName { get; set; } = null!;

    public int? IsActive { get; set; }
}
