using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class DottedLineEmailConfigHistory
{
    public int DottedLineEmailConfigHistoryId { get; set; }

    public int? HrRecordId { get; set; }

    public int? EmailNotificationTypeId { get; set; }

    public int? SendTypeId { get; set; }

    public int? CompanyId { get; set; }

    public int? BranchId { get; set; }

    public DateTime? HistoryCreateDate { get; set; }

    public int? CreateBy { get; set; }
}
