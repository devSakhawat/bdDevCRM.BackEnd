using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class AdditionalCtc
{
    public int CtcId { get; set; }

    public string CtcName { get; set; } = null!;

    /// <summary>
    /// 1=Allowance,2=Deduction
    /// </summary>
    public int CtcOperator { get; set; }
}
