using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class TempMultiApprover
{
    public string EmployeeId { get; set; } = null!;

    public string? RecommenderEmpId { get; set; }

    public string? ApproverEmpId { get; set; }

    public int? UserId { get; set; }

    public int? Sequence { get; set; }

    public int? ModuleId { get; set; }

    public int? GroupId { get; set; }
}
