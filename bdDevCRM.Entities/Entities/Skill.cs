using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class Skill
{
    public int SkillId { get; set; }

    public string? SkillName { get; set; }

    public int? IsActive { get; set; }
}
