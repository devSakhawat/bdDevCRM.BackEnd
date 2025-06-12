using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class CompetencyGradeMapping
{
    public int CompetencyGradeMappingId { get; set; }

    public int? CompetencyId { get; set; }

    public int? GradeId { get; set; }

    public int? CreateUser { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? UpdateUser { get; set; }

    public DateTime? UpdateDate { get; set; }
}
