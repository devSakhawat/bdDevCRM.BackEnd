using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class BdgBasicIncrement
{
    public int BasicIncrementId { get; set; }

    public int? KpiId { get; set; }

    public int? GradeId { get; set; }

    public decimal? Amount { get; set; }

    public int? BudgetYearId { get; set; }

    public int? IsHistory { get; set; }

    public int? IsActive { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public int? EmployeeTypeId { get; set; }
}
