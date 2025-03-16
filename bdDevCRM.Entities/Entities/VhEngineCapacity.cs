using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class VhEngineCapacity
{
    public int EngineCapacityId { get; set; }

    public string? Capacity { get; set; }

    public int? IsActive { get; set; }
}
