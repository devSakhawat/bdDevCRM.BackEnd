using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class BdgHalfYearlyKpiBehaviourGradeMap
{
    public int BdgHalfYearlyKpiBehaviourGradeMapId { get; set; }

    public int? BudgetYearId { get; set; }

    public int? EmployeeTypeId { get; set; }

    public int? GradeId { get; set; }

    public int? KpiId { get; set; }
}
