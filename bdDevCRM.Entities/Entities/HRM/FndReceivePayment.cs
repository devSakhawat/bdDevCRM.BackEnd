using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class FndReceivePayment
{
    public int ReceivePaymentId { get; set; }

    public int ReceivePaymentType { get; set; }

    public int? FiscalYearId { get; set; }

    public int? SubjectId { get; set; }

    public DateOnly FromDate { get; set; }

    public DateOnly ToDate { get; set; }

    public int AccountHeadId { get; set; }

    public decimal? CurrentYearAmount { get; set; }

    public decimal? PrevYearAmount { get; set; }

    public int? PostBy { get; set; }

    public DateTime? PostDate { get; set; }

    public int? AuditedBy { get; set; }

    public DateTime? AuditedDate { get; set; }

    public int Status { get; set; }
}
