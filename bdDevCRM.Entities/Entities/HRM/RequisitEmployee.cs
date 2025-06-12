using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class RequisitEmployee
{
    public int RequisitEmployeeId { get; set; }

    public int VehicleRequisitionId { get; set; }

    public int HrRecordId { get; set; }
}
