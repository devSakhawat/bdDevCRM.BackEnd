using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class VhRouteVehicleMap
{
    public int RouteAssignPassengerId { get; set; }

    public int? VehicleId { get; set; }

    public int? RouteId { get; set; }

    public int? IsActive { get; set; }
}
