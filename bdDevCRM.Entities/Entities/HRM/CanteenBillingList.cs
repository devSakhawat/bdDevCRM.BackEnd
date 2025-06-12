using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class CanteenBillingList
{
    public int CanteenBillingListId { get; set; }

    public int CanteenBillingId { get; set; }

    public DateTime MealDate { get; set; }

    public int CanteenId { get; set; }

    public int BranchId { get; set; }

    public int SupplierId { get; set; }

    public int? IsPaid { get; set; }

    public int MealTypeId { get; set; }

    public int MealPrice { get; set; }

    public int MealCount { get; set; }

    public int TotalPrice { get; set; }

    public int Penalty { get; set; }

    public virtual CanteenBilling CanteenBilling { get; set; } = null!;

    public virtual ICollection<CanteenBillingPenalty> CanteenBillingPenalty { get; set; } = new List<CanteenBillingPenalty>();

    public virtual ICollection<CanteenSupplierLedger> CanteenSupplierLedger { get; set; } = new List<CanteenSupplierLedger>();
}
