using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class BdgEmployeeForPerformenceByYear
{
    public int EmployeeForPerformenceByYearId { get; set; }

    public int? HrrecordId { get; set; }

    public int? YearId { get; set; }

    public int? IsActive { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? CreatedBy { get; set; }

    public int? IsUsed { get; set; }
}
