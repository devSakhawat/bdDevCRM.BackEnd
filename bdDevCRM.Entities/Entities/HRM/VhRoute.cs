using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class VhRoute
{
    public int RouteId { get; set; }

    public string? RouteName { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? BackTime { get; set; }

    public int? IsActive { get; set; }
}
