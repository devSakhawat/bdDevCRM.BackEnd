using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class VhPassengerRoute
{
    public int PassengerRouteId { get; set; }

    public string? PassengerRouteName { get; set; }

    public TimeOnly? StartTime { get; set; }

    public TimeOnly? BackTime { get; set; }

    public int? IsActive { get; set; }

    public int? BranchId { get; set; }
}
