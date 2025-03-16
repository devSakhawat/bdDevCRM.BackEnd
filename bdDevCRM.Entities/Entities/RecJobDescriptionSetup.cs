using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class RecJobDescriptionSetup
{
    public int JobDescriptionSetupId { get; set; }

    public int? DesignationId { get; set; }

    public string? JobResponsibilitiesA { get; set; }

    public string? RequiredSkillA { get; set; }

    public string? RequiredQualification { get; set; }

    public string? LanguageProficiency { get; set; }

    public string? ComputerLiteracy { get; set; }

    public int? AddedBy { get; set; }

    public DateTime? AddedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }
}
