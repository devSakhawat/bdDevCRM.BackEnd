using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class SalaryCostCentre
{
    public int SalaryCostCetreHistoryId { get; set; }

    public DateTime? SalaryMonth { get; set; }

    public int? HrRecordId { get; set; }

    public int? CostCentreId { get; set; }

    public int? CostShareRatio { get; set; }
}
