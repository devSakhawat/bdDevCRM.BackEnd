using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class NoticeCategory
{
    public int NoticeCategoryId { get; set; }

    public string? NoticeCategoryCode { get; set; }

    public string? NoticeCategoryDescription { get; set; }

    public int? IsActive { get; set; }
}
