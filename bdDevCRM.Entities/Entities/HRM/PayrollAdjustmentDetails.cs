using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class PayrollAdjustmentDetails
{
    public int PayrollAdjustmentDetailsId { get; set; }

    public int PayrollAdjustmentId { get; set; }

    public int CtcId { get; set; }

    public double CtcValue { get; set; }

    public DateTime ValidDateFrom { get; set; }

    public DateTime ValidDateTo { get; set; }

    public int UpdateBy { get; set; }

    public DateTime UpdateDate { get; set; }

    public int IsAdditional { get; set; }

    public int AddRemove { get; set; }

    public string? AccountNo { get; set; }

    public string? Remarks { get; set; }
}
