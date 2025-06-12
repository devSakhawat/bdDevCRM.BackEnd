using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class VhVehicleRegistration
{
    public int VehicleId { get; set; }

    public DateTime? EffectiveDate { get; set; }

    public int? OwnershipId { get; set; }

    public int? ManufactureId { get; set; }

    public int? VehicleTypeId { get; set; }

    public int? UsageTypeId { get; set; }

    public int? EngineCapacityId { get; set; }

    public int? ProviderId { get; set; }

    public int? FuelTypeId { get; set; }

    public int? ModelId { get; set; }

    public string? RegistrationNumber { get; set; }

    public DateTime? Registrationdate { get; set; }

    public string? Price { get; set; }

    public string? Currency { get; set; }

    public string? Weight { get; set; }

    public string? WeightUnit { get; set; }

    public string? SeatCapacity { get; set; }

    public int? ColorId { get; set; }

    public string? Remarks { get; set; }

    public int? Status { get; set; }

    public DateTime? NxtFitnessExpDate { get; set; }

    public DateTime? NxtTaxExpDate { get; set; }

    public DateTime? NxtInsuranceExpDate { get; set; }

    public string? ChassisNo { get; set; }

    public string? EngineNo { get; set; }

    public DateTime? WarrantyDate { get; set; }

    public int? CompanyId { get; set; }

    public int? BranchId { get; set; }

    public int? AllocationStatus { get; set; }

    public int? AddedBy { get; set; }

    public DateTime? AddedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? ApprovedBy { get; set; }

    public bool? IsApproved { get; set; }

    public DateTime? ApprovedDate { get; set; }

    public int? PollAllocationStatus { get; set; }

    public int? IsSold { get; set; }

    public string? RegistrationDocument { get; set; }
}
