using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class AttributeDepartmentMapping
{
    public int AttributeDepartmentMappingId { get; set; }

    public int? PerformanceAttributeId { get; set; }

    public int? DepartmentId { get; set; }
}
