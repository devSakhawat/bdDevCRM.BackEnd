using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class CanteenSupplierLedger
{
    public int SupplierLedgerId { get; set; }

    public int CanteenId { get; set; }

    public int BranchId { get; set; }

    public int SupplierId { get; set; }

    public int? CanteenBillingListId { get; set; }

    public DateTime SupplierLedgerDate { get; set; }

    public string? LedgerReferenceNumber { get; set; }

    public int? PaymentType { get; set; }

    public string? SupplierLedgerDescription { get; set; }

    public int DebitAmount { get; set; }

    public int CreditAmount { get; set; }

    public int? Balance { get; set; }

    public virtual CanteenBillingList? CanteenBillingList { get; set; }
}
