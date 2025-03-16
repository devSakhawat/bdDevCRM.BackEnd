using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class TaskType
{
    public int TaskTypeId { get; set; }

    public string? TaskTypeName { get; set; }

    public int? IsMatricsActive { get; set; }

    public int? IsRevisionActive { get; set; }

    public int? IsActive { get; set; }
}
