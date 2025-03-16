using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class AccountHead
{
    public int AccountHeadId { get; set; }

    public string AccountHeadName { get; set; } = null!;

    public string AccountHeadCode { get; set; } = null!;

    /// <summary>
    /// 1= Balance Sheet , 2= Profit &amp; Loss
    /// </summary>
    public int AccountHeadType { get; set; }

    public int? ParentAccountHeadId { get; set; }

    public int? HasSubsidiaryLedger { get; set; }

    public int? LedgerHeadId { get; set; }

    public int? IsActive { get; set; }

    /// <summary>
    /// 1=Debit Type,2=Credit Type
    /// </summary>
    public int? TransectionType { get; set; }

    public int? IsConSubLg { get; set; }

    public int? ConvertSubType { get; set; }

    public int? SubLgId { get; set; }

    public string? SubLgCode { get; set; }

    public int? Level { get; set; }

    public int? IsPl { get; set; }

    public int? IsCashFlow { get; set; }

    public int? IsAllowNegetive { get; set; }

    public int? IsManualHead { get; set; }
}
