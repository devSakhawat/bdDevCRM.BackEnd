using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class EmployeeMembership
{
    public int EmpMembershipId { get; set; }

    public int? HrRecordId { get; set; }

    public string? MembershipName { get; set; }

    public string? Activity { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }
}
