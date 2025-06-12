using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class VhVehicleRequisition
{
    public int VehicleRequisitionId { get; set; }

    public int RquisitionTypeId { get; set; }

    public DateOnly PreferedStartDate { get; set; }

    public TimeOnly? PreferStartTime { get; set; }

    public DateOnly PreferedEndDate { get; set; }

    public TimeOnly? PreferedEndTime { get; set; }

    public string PickupSpot { get; set; } = null!;

    public string Destination { get; set; } = null!;

    public int NoOfTravelers { get; set; }

    public string? MobileNo { get; set; }

    public string? PabxExt { get; set; }

    public decimal? Miles { get; set; }

    public int StateId { get; set; }

    public bool IsActive { get; set; }

    public DateTime? ApplyDate { get; set; }

    public bool? IsSecondOrSixDay { get; set; }

    public bool? IsReadyToAllocation { get; set; }

    public int? BranchId { get; set; }

    public int? ApplyedByHrRecordId { get; set; }

    public int? ApprovedBy { get; set; }

    public DateTime? ApprovedDate { get; set; }

    public bool? IsApproved { get; set; }

    public int? RejectedBy { get; set; }

    public DateTime? RejectedDate { get; set; }

    public int? CancelledBy { get; set; }

    public DateTime? CancelledDate { get; set; }
}
