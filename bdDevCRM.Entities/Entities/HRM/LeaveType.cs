using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class LeaveType
{
    public int LeaveTypeId { get; set; }

    public string? Typename { get; set; }

    public string? Leavetypecode { get; set; }

    public int? Isactive { get; set; }

    public int? IsRemarksApplicable { get; set; }
}
