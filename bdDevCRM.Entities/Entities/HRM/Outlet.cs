using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class Outlet
{
    public int OutletId { get; set; }

    public string? OutletName { get; set; }

    public string? OutletCode { get; set; }

    public string? OutletAddress { get; set; }

    public string? ContactPerson { get; set; }

    public string? ContactNo { get; set; }

    public string? GeoLocation { get; set; }

    public int? RsmregionId { get; set; }
}
