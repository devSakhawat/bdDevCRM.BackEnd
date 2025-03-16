using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class LoanEarlySettlement
{
    public int LoanSettlementId { get; set; }

    public int? LoanId { get; set; }

    public int? HrRecordId { get; set; }

    public int? BankId { get; set; }

    public int? BranchId { get; set; }

    public decimal? Amount { get; set; }

    public string? DepositChkNo { get; set; }

    public DateOnly? DepositDate { get; set; }

    public string? Attachment { get; set; }

    public int? ApprovalStatus { get; set; }

    public string? VoucherNo { get; set; }

    public string? EarlySettlementRemarks { get; set; }
}
