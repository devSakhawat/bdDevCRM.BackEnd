using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class KraAssignApprover
{
    public int KraAssignApproverId { get; set; }

    public int KraYearId { get; set; }

    public int KraApproverId { get; set; }

    public int HrRecordId { get; set; }

    public int? IsNew { get; set; }

    public int? IsApprove { get; set; }
}
