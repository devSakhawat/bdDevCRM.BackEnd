using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class MobileVendorLedger
{
    public int VendorLedgerId { get; set; }

    public int SimVendorId { get; set; }

    public string? LedgerReferenceNumber { get; set; }

    public int? PaymentType { get; set; }

    public string? VendorLedgerDescription { get; set; }

    public decimal DebitAmount { get; set; }

    public decimal CreditAmount { get; set; }

    public decimal? Balance { get; set; }

    public DateTime? MobileVendorLedgerDate { get; set; }
}
