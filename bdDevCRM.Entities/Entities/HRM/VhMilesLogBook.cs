using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class VhMilesLogBook
{
    public int MilesLogBookId { get; set; }

    public DateTime? StartingDate { get; set; }

    public string? StartMiles { get; set; }

    public DateTime? EndDate { get; set; }

    public string? EndMiles { get; set; }

    public int? VehicleId { get; set; }

    public int? DriverId { get; set; }

    public int? AddedBy { get; set; }

    public string? AddedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public string? UpdatedDate { get; set; }

    public bool? IsApproved { get; set; }

    public int? ApprovedBy { get; set; }

    public string? ApprovedDate { get; set; }
}
