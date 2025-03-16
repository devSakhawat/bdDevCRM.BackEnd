using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class AlternativeLeaveMap
{
    public int AlternativeLeaveMapId { get; set; }

    public int? AlternativeLeaveId { get; set; }

    public int? LeaveId { get; set; }

    public int? LeaveTypeId { get; set; }
}
