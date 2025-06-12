using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class FndTrialBalance
{
    public int TrialBalanceId { get; set; }

    public int? FiscalYearId { get; set; }

    public int? SubjectId { get; set; }

    public DateOnly FromDate { get; set; }

    public DateOnly ToDate { get; set; }

    public int AccountHeadType { get; set; }

    public int? TransectionType { get; set; }

    public int ParentAccountHeadId { get; set; }

    public int AccountHeadId { get; set; }

    public decimal? OpeningDebit { get; set; }

    public decimal? OpeningCredit { get; set; }

    public decimal? PeriodDebit { get; set; }

    public decimal? PeriodCredit { get; set; }

    public decimal? ClosingDebit { get; set; }

    public decimal? ClosingCredit { get; set; }

    public int? PostBy { get; set; }

    public DateTime? PostDate { get; set; }

    public int? AuditedBy { get; set; }

    public DateTime? AuditedDate { get; set; }

    public int Status { get; set; }
}
