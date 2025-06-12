using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class ProrataBonusForEmployeeType
{
    public int ProrataBonusForEmployeeTypeId { get; set; }

    public int? CtcId { get; set; }

    public int? EmployeeTypeId { get; set; }
}
