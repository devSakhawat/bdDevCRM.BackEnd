using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class IvmsVisitDetails
{
    public int VisitDetailsId { get; set; }

    public int VisitorId { get; set; }

    public int VisiteeId { get; set; }

    public string PassId { get; set; } = null!;

    public DateTime? TimeFrom { get; set; }

    public DateTime? TimeTo { get; set; }

    public DateOnly? ValidFrom { get; set; }

    public DateOnly? ValidTo { get; set; }

    public string? AreaAllowed { get; set; }

    public string? VehicleNo { get; set; }
}
