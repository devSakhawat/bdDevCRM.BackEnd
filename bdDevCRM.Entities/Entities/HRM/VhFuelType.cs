using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class VhFuelType
{
    public int FuelTypeId { get; set; }

    public string? FuelTypeName { get; set; }

    public int? IsActive { get; set; }
}
