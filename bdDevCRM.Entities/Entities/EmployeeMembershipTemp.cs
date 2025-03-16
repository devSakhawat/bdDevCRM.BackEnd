using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class EmployeeMembershipTemp
{
    public int EmpMembershipTempId { get; set; }

    public string? EmployeeId { get; set; }

    public string? MembershipName { get; set; }

    public string? Activity { get; set; }

    public string? StartDate { get; set; }

    public string? EndDate { get; set; }

    public int? UserId { get; set; }
}
