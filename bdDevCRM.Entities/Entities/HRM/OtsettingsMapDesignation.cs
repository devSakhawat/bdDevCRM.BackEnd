using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class OtsettingsMapDesignation
{
    public int OtsettingsMapDesignationId { get; set; }

    public int? OverTimeId { get; set; }

    public int? DesignationId { get; set; }
}
