using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class CanteenBillingPenalty
{
    public int CanteenPenaltyId { get; set; }

    public int CanteenBillingId { get; set; }

    public DateTime PenaltyDate { get; set; }

    public string PenaltyReason { get; set; } = null!;

    public int? NumberOfMeal { get; set; }

    public int? MealTypeId { get; set; }

    public int? FixedAmount { get; set; }

    public int PenaltyType { get; set; }

    public int CanteenBillingListId { get; set; }

    public virtual CanteenBilling CanteenBilling { get; set; } = null!;

    public virtual CanteenBillingList CanteenBillingList { get; set; } = null!;
}
