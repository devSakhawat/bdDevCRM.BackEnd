using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class ProjectTask
{
    public int TaskId { get; set; }

    public int? ProjectId { get; set; }

    public string? TaskName { get; set; }

    public string? TaskDescription { get; set; }

    public int? TaskType { get; set; }

    public int? Isactive { get; set; }
}
