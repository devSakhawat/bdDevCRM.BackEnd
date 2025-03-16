using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class VhAssignVhToDivision
{
    public int AssignToDivisionId { get; set; }

    public int? DivisionId { get; set; }

    public int? DepartmentId { get; set; }

    public DateTime? AssignDate { get; set; }

    public int? VehicleId { get; set; }

    public int? IsActive { get; set; }

    public int? AddedBy { get; set; }

    public DateTime? AddedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public bool? IsApproved { get; set; }

    public int? ApprovedBy { get; set; }

    public DateTime? ApprovedDate { get; set; }
}
