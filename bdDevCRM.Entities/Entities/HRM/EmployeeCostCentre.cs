using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class EmployeeCostCentre
{
    public int EmployeeCcid { get; set; }

    public int HrRecordId { get; set; }

    public int? CostCentreId { get; set; }

    public int? CostShareRatio { get; set; }

    public DateTime? LastUpdateDate { get; set; }

    public int? IsActive { get; set; }

    public int? CostCompanyId { get; set; }
}
