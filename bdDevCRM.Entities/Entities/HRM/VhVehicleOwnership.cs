using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class VhVehicleOwnership
{
    public int OwnershipId { get; set; }

    public string? OwnershipName { get; set; }

    public int? IsActive { get; set; }
}
