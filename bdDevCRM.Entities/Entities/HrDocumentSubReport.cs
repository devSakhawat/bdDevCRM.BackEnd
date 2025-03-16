using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class HrDocumentSubReport
{
    public int HrDocumentSubId { get; set; }

    public string? SubReportName { get; set; }

    public int? HrDocumentId { get; set; }

    public int? ReportHeaderId { get; set; }

    public int? DataSourceId { get; set; }
}
