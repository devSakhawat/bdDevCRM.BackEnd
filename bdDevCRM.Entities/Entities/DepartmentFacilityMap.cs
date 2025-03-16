using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class DepartmentFacilityMap
{
    public int DeptFacilityMapId { get; set; }

    public int? DepartmentId { get; set; }

    public int? FacilityId { get; set; }

    public int? CompanyId { get; set; }
}
