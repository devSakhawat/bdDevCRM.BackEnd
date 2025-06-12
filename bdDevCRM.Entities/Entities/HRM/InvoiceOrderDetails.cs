using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class InvoiceOrderDetails
{
    public int InvoiceOrderId { get; set; }

    public int? InvoiceId { get; set; }

    public string? Describtion { get; set; }

    public decimal? PaidAmount { get; set; }

    public decimal? Amount { get; set; }

    public int? GroupNo { get; set; }
}
