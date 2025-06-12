using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class EmployeeSkillSet
{
    public int EmployeeSkillSetId { get; set; }

    public int? HrRecordId { get; set; }

    public int? SkillId { get; set; }

    public string? OtherSkill { get; set; }
}
