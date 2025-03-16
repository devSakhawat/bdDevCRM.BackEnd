using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class BalanceSheet
{
    public int BalanceSheetId { get; set; }

    public int SubjectId { get; set; }

    public int FinancialYearId { get; set; }

    public DateOnly FromDate { get; set; }

    public DateOnly ToDate { get; set; }

    public int? AccountHeadId { get; set; }

    public string? AccountHeadCode { get; set; }

    public string? AccountHeadName { get; set; }

    public decimal? CurrentYearAmt { get; set; }

    public decimal? PrevYearAmt { get; set; }

    public int? StateId { get; set; }

    public int? PostedBy { get; set; }

    public DateOnly? PostedDate { get; set; }

    public int? AuditedBy { get; set; }

    public DateOnly? AuditedDate { get; set; }

    public int? AccountHeadType { get; set; }

    public int? TransectionType { get; set; }
}
