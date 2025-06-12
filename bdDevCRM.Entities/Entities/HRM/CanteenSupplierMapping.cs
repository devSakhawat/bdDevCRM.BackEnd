using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class CanteenSupplierMapping
{
    public int CanteenSupplierMappingId { get; set; }

    public int CanteenId { get; set; }

    public int SupplierId { get; set; }

    public bool IsActive { get; set; }
}
