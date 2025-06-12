using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class HospitalInformation
{
    public int HospitalId { get; set; }

    public string? HospitalName { get; set; }

    public int? IsActive { get; set; }
}
