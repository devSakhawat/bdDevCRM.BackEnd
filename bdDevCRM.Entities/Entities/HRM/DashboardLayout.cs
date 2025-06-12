using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class DashboardLayout
{
    public int LayoutId { get; set; }

    public string? LayoutTitle { get; set; }

    public int? LayoutColumns { get; set; }

    public int? IsActive { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }
}
