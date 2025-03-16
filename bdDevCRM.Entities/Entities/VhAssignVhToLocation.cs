using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class VhAssignVhToLocation
{
    public int AssignId { get; set; }

    public int? LocationId { get; set; }

    public int? VehicleId { get; set; }

    public DateOnly? AssignDate { get; set; }

    public int? RouteId { get; set; }

    public int? AddedBy { get; set; }

    public DateOnly? AddedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public DateOnly? UpdatedDate { get; set; }

    public bool? IsApproved { get; set; }

    public int? ApprovedBy { get; set; }

    public DateOnly? ApprovedDate { get; set; }

    public int? IsActive { get; set; }
}
