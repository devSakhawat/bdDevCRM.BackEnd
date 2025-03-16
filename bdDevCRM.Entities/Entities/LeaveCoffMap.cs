using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class LeaveCoffMap
{
    public int LeaveCoffMapId { get; set; }

    public int? CoffId { get; set; }

    public int? LeaveId { get; set; }
}
