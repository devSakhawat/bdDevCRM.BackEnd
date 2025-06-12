using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class LwpDeductionSettings
{
    public int? LwpDeductionPolicyId { get; set; }

    public int? EmployeeTypeId { get; set; }

    public decimal? DeductionPer { get; set; }
}
