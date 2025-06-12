using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class VhManufacture
{
    public int ManufactureId { get; set; }

    public string? ManufactureName { get; set; }

    public int? IsActive { get; set; }
}
