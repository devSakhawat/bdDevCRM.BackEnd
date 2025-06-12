using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class GradeType
{
    public int GradeTypeId { get; set; }

    public string GradeTypeName { get; set; } = null!;

    public bool? IsActive { get; set; }

    public int? CompanyId { get; set; }

    public int? Sorder { get; set; }

    public virtual ICollection<CompetencyLevelAndGradeMap> CompetencyLevelAndGradeMap { get; set; } = new List<CompetencyLevelAndGradeMap>();
}
