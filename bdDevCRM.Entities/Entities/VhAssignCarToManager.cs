using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class VhAssignCarToManager
{
    public int AssignCarToManagerId { get; set; }

    public int? VehicleId { get; set; }

    public int? HrrecordId { get; set; }

    public DateTime? AssignCarToManagerDate { get; set; }

    public int? IsActive { get; set; }

    public int? AddedBy { get; set; }

    public DateTime? AddedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public bool? IsApproved { get; set; }

    public int? ApprovedBy { get; set; }

    public DateTime? ApprovedDate { get; set; }

    public string? RegistrationNumber { get; set; }

    public int? EmployeeId { get; set; }
}
