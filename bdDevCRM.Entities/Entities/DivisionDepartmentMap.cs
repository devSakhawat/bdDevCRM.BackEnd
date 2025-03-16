using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class DivisionDepartmentMap
{
    public int DivisonDeptMapId { get; set; }

    public int? CompanyId { get; set; }

    public int? DivisionId { get; set; }

    public int? DepartmentId { get; set; }
}
