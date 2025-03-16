using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class Notice
{
    public int NoticeId { get; set; }

    public int? NoticeCategoryId { get; set; }

    public string? NoticeTitle { get; set; }

    public string? NoticeDetails { get; set; }

    public DateTime? Publishdate { get; set; }

    public DateTime? ExpiryDate { get; set; }

    public int? IsAnnonymous { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? IsSentEmailForNotice { get; set; }
}
