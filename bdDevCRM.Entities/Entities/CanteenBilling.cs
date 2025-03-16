using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class CanteenBilling
{
    public int CanteenBillingId { get; set; }

    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public int CanteenId { get; set; }

    public int BranchId { get; set; }

    public int SupplierId { get; set; }

    public int TotalMeal { get; set; }

    public int TotalPrice { get; set; }

    public int? PenaltyAmount { get; set; }

    public int NetPay { get; set; }

    public int? IsPenalty { get; set; }

    public string? Remarks { get; set; }

    public virtual ICollection<CanteenBillingList> CanteenBillingList { get; set; } = new List<CanteenBillingList>();

    public virtual ICollection<CanteenBillingPenalty> CanteenBillingPenalty { get; set; } = new List<CanteenBillingPenalty>();
}
