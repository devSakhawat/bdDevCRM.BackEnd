using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class RosterRollingGroupTemp
{
    public int RosterRollingGroupTempId { get; set; }

    public string? EmployeeId { get; set; }

    public string? RollingGroupName { get; set; }

    public string? Status { get; set; }

    public string? IsValid { get; set; }

    public int? UserId { get; set; }
}
