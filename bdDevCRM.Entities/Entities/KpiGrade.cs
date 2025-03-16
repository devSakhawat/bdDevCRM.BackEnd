using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class KpiGrade
{
    public int KpiGradeId { get; set; }

    public string KpiGradeName { get; set; } = null!;

    public int KpiGradeMarks { get; set; }

    public int Status { get; set; }

    public int? SortOrder { get; set; }
}
