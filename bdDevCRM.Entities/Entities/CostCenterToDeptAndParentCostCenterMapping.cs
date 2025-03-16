using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class CostCenterToDeptAndParentCostCenterMapping
{
    public int CostCenterToDeptAndParentCostCenterMappingId { get; set; }

    public string? LocationCode { get; set; }

    public string? LocationName { get; set; }

    public int? DepartmentId { get; set; }

    public string? DepartmentName { get; set; }

    public string? CostCenter { get; set; }
}
