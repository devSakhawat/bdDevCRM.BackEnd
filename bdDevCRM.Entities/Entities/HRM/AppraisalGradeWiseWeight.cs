using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class AppraisalGradeWiseWeight
{
    public int WeightId { get; set; }

    public int? CompetencyId { get; set; }

    public int? Weight { get; set; }

    public int? GradeId { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateUser { get; set; }

    public int? IsDepartmentHead { get; set; }
}
