using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class Facility
{
    public int FacilityId { get; set; }

    public string? FacilityCode { get; set; }

    public string? FacilityName { get; set; }

    public int? IsActive { get; set; }
}
