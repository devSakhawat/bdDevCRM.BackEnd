using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class KraYearAndGradeMap
{
    public int KraYearAndGradeMapId { get; set; }

    public int YearConfigId { get; set; }

    public int CompanyId { get; set; }

    public int GradeTypeId { get; set; }

    public int? HrRecordId { get; set; }
}
