using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class LeaveDedDetails
{
    public int LeaveDedDetailsId { get; set; }

    public int LeaveDedPolicyId { get; set; }

    public int LeaveTypeId { get; set; }

    public int SortOrder { get; set; }
}
