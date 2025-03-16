using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class DepartmentSectionMap
{
    public int DeptSectionId { get; set; }

    public int? DepartmentId { get; set; }

    public int? SectionId { get; set; }

    public int? CompanyId { get; set; }
}
