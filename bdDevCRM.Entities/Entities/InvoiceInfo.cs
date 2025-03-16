using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class InvoiceInfo
{
    public int InvoiceId { get; set; }

    public DateTime? InvoiceDate { get; set; }

    public string? InvoiceNo { get; set; }

    public string? CustomerCode { get; set; }

    public int? PurchaseOrderId { get; set; }

    public DateTime? PurchaseOrderDate { get; set; }

    public int? InvoiceApproverId { get; set; }

    public int? ClientId { get; set; }

    public int? ContactId { get; set; }

    public string? Address { get; set; }

    public decimal? SubTotal { get; set; }

    public decimal? TaxRate { get; set; }

    public decimal? TaxAmount { get; set; }

    public decimal? Discount { get; set; }

    public decimal? Total { get; set; }

    public int? ChkPayTo { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? InvPrintBy { get; set; }

    public DateTime? InvPrintDate { get; set; }

    public int? IsActive { get; set; }

    public int? PrintStatus { get; set; }

    public string? Comments { get; set; }

    public int? ImageReceive { get; set; }
}
