using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class Investment
{
    public int InvestmentId { get; set; }

    public int? SubjectId { get; set; }

    public int InvestmentType { get; set; }

    public int BankId { get; set; }

    public int BranchId { get; set; }

    public string AccountNo { get; set; } = null!;

    public decimal? Amount { get; set; }

    public decimal? InterestRate { get; set; }

    public int? InterestMode { get; set; }

    public DateOnly? MaturityDate { get; set; }

    public DateOnly? PurchaseDate { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? IsFinalPost { get; set; }

    public DateTime? PostingDate { get; set; }

    public int? PostedBy { get; set; }

    public string? InvestAccCode { get; set; }

    public decimal? PrincipalAmt { get; set; }

    public string? ProfitAccCode { get; set; }

    public decimal? ProfitAmt { get; set; }

    public decimal? ActualProfitRate { get; set; }

    public string? TrusteeBankAccCode { get; set; }

    public int? Status { get; set; }

    public string? VoucherNo { get; set; }

    public string? SubLedgerAccountCode { get; set; }

    public int? TotalDays { get; set; }

    public int? RenewalRef { get; set; }

    public string? InvestmentCode { get; set; }

    public int? MatureEmailStatus { get; set; }
}
