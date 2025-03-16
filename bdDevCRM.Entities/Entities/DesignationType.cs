using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class DesignationType
{
    public int DesignationTypeId { get; set; }

    public string TypeName { get; set; } = null!;
}
