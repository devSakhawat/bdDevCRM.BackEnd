using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class VhEmployeePickUpSpot
{
    public int PickUpSpotId { get; set; }

    public int? HrrecordId { get; set; }

    public string? PickUpSpotName { get; set; }

    public int? IsActive { get; set; }
}
