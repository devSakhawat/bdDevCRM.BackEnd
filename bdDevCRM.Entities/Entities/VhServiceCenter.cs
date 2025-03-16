using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class VhServiceCenter
{
    public int ServiceCenterId { get; set; }

    public string? ServiceCenterName { get; set; }

    public int? IsActive { get; set; }
}
