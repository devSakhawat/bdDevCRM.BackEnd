using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class RosterFwdEmployeeDetails
{
    public int RosterFwdEmployeeDetailsId { get; set; }

    public int? RosterFwdEmpId { get; set; }

    public int? HrRecordId { get; set; }
}
