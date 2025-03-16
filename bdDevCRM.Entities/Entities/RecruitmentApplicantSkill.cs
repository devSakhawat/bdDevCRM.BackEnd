using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class RecruitmentApplicantSkill
{
    public int ApplicantSkillId { get; set; }

    public int? SkillId { get; set; }

    public int? ApplicantId { get; set; }

    public string? OtherSkill { get; set; }
}
