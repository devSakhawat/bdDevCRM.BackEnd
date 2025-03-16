using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class Venue
{
    public int VenueId { get; set; }

    public string? VenueName { get; set; }

    public string? VenueCode { get; set; }

    public int? Status { get; set; }

    public string? VenueDetails { get; set; }
}
