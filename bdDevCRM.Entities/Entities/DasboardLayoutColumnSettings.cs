using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class DasboardLayoutColumnSettings
{
    public int DlColumnId { get; set; }

    public int? LayoutId { get; set; }

    public string? CssTitleName { get; set; }

    public string? CssRatio { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }
}
