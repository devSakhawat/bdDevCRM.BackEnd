using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class EmployeeCostCenterTemp
{
    public int EmployeeCctempId { get; set; }

    public string? EmployeeId { get; set; }

    public string? CostCentreCode { get; set; }

    public string? CostShareRatio { get; set; }

    public int? UserId { get; set; }
}
