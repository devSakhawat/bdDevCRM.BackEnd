using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class PayrollBlockInfo
{
    public int PayrollBlockId { get; set; }

    public int? HrRecordId { get; set; }

    public DateTime? BlockDate { get; set; }

    public DateTime? UnblockDate { get; set; }

    public int? BlockBy { get; set; }

    public int? UnblockBy { get; set; }

    /// <summary>
    /// 1=Block,0=Unblock
    /// </summary>
    public int? BlockStatus { get; set; }

    public int? IsBlockForBonus { get; set; }

    public int? BonusBlockStatus { get; set; }

    public string? BlockRemark { get; set; }

    public string? UnBlockRemark { get; set; }

    public DateTime? BlockSubmitDate { get; set; }

    public DateTime? UnBlockSubmitDate { get; set; }

    public int? IsNotified { get; set; }
}
