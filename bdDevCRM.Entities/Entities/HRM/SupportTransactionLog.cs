using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class SupportTransactionLog
{
    public int TransactionLogId { get; set; }

    public DateTime TransactionDate { get; set; }

    public int? TransactionTypeId { get; set; }

    public string TransactionType { get; set; } = null!;

    public int? ResponseCode { get; set; }

    public string? Request { get; set; }

    public string? Response { get; set; }

    public string? Remarks { get; set; }
}
