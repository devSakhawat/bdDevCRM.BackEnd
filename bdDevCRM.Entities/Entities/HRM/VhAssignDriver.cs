using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class VhAssignDriver
{
    public int AssignId { get; set; }

    public int? DriverId { get; set; }

    public string? AssignDateFrom { get; set; }

    public string? AssignDateTo { get; set; }

    public bool? IsActive { get; set; }

    public int? VehicleId { get; set; }
}
