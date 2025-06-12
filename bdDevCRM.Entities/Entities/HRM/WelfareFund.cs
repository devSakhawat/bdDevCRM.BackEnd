using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class WelfareFund
{
    public int WelfareFundId { get; set; }

    public int? HrRecordId { get; set; }

    public int? AccountHeadId { get; set; }

    public string? Particular { get; set; }

    public DateOnly? TransactionDate { get; set; }

    public decimal? AmountDr { get; set; }

    public decimal? AmountCr { get; set; }

    public decimal? Balance { get; set; }

    public int? IsOpening { get; set; }

    public int? UpdateBy { get; set; }
}
