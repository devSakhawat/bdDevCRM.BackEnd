using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class OmitLateLog
{
    public int OmitLateLogId { get; set; }

    public int? HrRecordId { get; set; }

    public DateTime? Date { get; set; }

    public int? UserId { get; set; }

    public string? Status { get; set; }

    public string? Type { get; set; }
}
