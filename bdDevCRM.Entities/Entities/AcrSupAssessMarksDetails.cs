using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class AcrSupAssessMarksDetails
{
    public int Id { get; set; }

    public int AcrId { get; set; }

    public int KpiId { get; set; }

    public int SubKpiId { get; set; }

    public int KpiGradeId { get; set; }

    public DateTime LastUpdateDate { get; set; }
}
