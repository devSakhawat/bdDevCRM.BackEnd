using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class District
{
    public int DistrictId { get; set; }

    public string? DistrictName { get; set; }

    public string? DistrictCode { get; set; }

    public int? Status { get; set; }

    public string? DistrictNameBn { get; set; }
}
