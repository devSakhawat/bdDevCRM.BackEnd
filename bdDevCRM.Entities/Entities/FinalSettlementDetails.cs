using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class FinalSettlementDetails
{
    public int SettlementDetailId { get; set; }

    public int SettlementId { get; set; }

    public int? CtcId { get; set; }

    public decimal? CtcValue { get; set; }

    public int? SettlementType { get; set; }

    public string? Remarks { get; set; }
}
