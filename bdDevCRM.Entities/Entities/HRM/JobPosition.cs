using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class JobPosition
{
    public int JobPositionId { get; set; }

    public string? JobPositionName { get; set; }

    public int? IsActive { get; set; }
}
