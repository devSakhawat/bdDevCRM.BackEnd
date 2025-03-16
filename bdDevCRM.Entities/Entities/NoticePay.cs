using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class NoticePay
{
    public int NoticePayId { get; set; }

    public int HrRecordId { get; set; }

    public decimal? TotalBasic { get; set; }

    public int? CountOfBasic { get; set; }

    public DateTime? NoticeReceiveDate { get; set; }

    public int? StateId { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? ApproveBy { get; set; }

    public DateTime? ApproveDate { get; set; }

    public int? DeleteBy { get; set; }

    public DateTime? DeleteDate { get; set; }

    public string? Remaks { get; set; }
}
