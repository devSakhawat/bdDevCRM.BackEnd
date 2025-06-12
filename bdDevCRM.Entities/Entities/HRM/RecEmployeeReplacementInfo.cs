using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class RecEmployeeReplacementInfo
{
    public int NewReplacementId { get; set; }

    public int? ReplacementHrRecordId { get; set; }

    public int? JobId { get; set; }
}
