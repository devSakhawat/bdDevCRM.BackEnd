using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class VhVehileRouteAssignToPassengerMapDetails
{
    public int RouteEmployeeId { get; set; }

    public int? HrRecordId { get; set; }

    public int? RouteAssignPassengerId { get; set; }

    public int? IsActive { get; set; }
}
