using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class JobEndType
{
    public int JobEndTypeId { get; set; }

    public string? JobEndTypeName { get; set; }

    public int? Status { get; set; }
}
