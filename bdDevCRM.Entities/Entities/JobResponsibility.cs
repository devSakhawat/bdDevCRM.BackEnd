using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class JobResponsibility
{
    public int ResponsibilityId { get; set; }

    public string? Responsibility { get; set; }

    public bool? IsActive { get; set; }
}
