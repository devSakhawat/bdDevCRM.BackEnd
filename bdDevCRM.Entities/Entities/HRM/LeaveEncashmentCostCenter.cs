using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class LeaveEncashmentCostCenter
{
    public int LeaveEncashmentCostId { get; set; }

    public int? YearId { get; set; }

    public int? HrRecordId { get; set; }

    public int? CostCentreId { get; set; }
}
