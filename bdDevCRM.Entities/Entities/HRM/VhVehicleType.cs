using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class VhVehicleType
{
    public int VehicleTypeId { get; set; }

    public string? VehicleTypeName { get; set; }

    public int? IsActive { get; set; }
}
