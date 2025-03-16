using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class VhRequisitionAllocation
{
    public int AllocationId { get; set; }

    public int RequisitionId { get; set; }

    public int VehicleId { get; set; }

    public int DriverId { get; set; }

    public int? AddedBy { get; set; }

    public DateTime? AddedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public bool? IsApproved { get; set; }

    public int? ApprovedBy { get; set; }

    public DateTime? ApprovedDate { get; set; }

    public DateTime? ReleaseDate { get; set; }
}
