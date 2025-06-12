using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class EmployeeSkilsTemp
{
    public int EmployeeSkilsTempId { get; set; }

    public string? EmployeeId { get; set; }

    public string? Skill { get; set; }

    public string? OtherSkill { get; set; }

    public int? UserId { get; set; }
}
