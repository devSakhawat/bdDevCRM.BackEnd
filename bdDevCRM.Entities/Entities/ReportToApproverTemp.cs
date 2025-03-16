using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class ReportToApproverTemp
{
    public int ReportToApproverId { get; set; }

    public string? EmployeeId { get; set; }

    public string? ReportTo { get; set; }

    public string? Approver { get; set; }
}
