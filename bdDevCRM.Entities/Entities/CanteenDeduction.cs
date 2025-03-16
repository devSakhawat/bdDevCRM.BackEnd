using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class CanteenDeduction
{
    public int EmployeeDeductionId { get; set; }

    public int HrrecordId { get; set; }

    public int DeductionYear { get; set; }

    public int DeductionMonth { get; set; }

    public int MealCount { get; set; }

    public int TotalPrice { get; set; }

    public int EmployeeContribution { get; set; }

    public int CompanyContribution { get; set; }

    public int IsPrecessed { get; set; }

    public int CanteenId { get; set; }

    public int BranchId { get; set; }
}
