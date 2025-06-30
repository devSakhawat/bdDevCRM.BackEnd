using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities.System;

public partial class ReportBuilder
{
    public int ReportHeaderId { get; set; }

    public string? ReportHeader { get; set; }

    public string? ReportTitle { get; set; }

    public int? QueryType { get; set; }

    public string? QueryText { get; set; }

    public int? IsActive { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? OrderByColumn { get; set; }
}
