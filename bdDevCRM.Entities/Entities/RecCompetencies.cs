using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class RecCompetencies
{
    public int Id { get; set; }

    public string? CompetencyName { get; set; }

    public int? CompetencyType { get; set; }

    public int? IsDepartmentHead { get; set; }

    public int? IsActive { get; set; }
}
