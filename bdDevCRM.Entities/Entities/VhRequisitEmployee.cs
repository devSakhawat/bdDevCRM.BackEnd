using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class VhRequisitEmployee
{
    public int RequisitEmployeeId { get; set; }

    public int VehicleRequisitionId { get; set; }

    public int HrRecordId { get; set; }

    public bool? IsAllocated { get; set; }

    public int? VehicleId { get; set; }

    public int? DriverId { get; set; }
}
