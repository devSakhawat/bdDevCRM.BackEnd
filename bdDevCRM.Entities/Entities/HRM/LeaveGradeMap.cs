using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class LeaveGradeMap
{
    public int LeaveDesignationMapId { get; set; }

    public int? LeavePolicyId { get; set; }

    public int? GradeId { get; set; }
}
