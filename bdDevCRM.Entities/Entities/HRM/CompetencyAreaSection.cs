using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class CompetencyAreaSection
{
    public int Id { get; set; }

    public int CompetencyAreaId { get; set; }

    public string? CompAreaSectionName { get; set; }
}
