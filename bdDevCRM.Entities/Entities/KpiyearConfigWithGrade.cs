using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class KpiyearConfigWithGrade
{
    public int KpiYearGradeMapId { get; set; }

    public int? YearConfigId { get; set; }

    public int? GradeId { get; set; }
}
