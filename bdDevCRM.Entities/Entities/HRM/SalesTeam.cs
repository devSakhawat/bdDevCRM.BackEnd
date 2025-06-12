using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class SalesTeam
{
    public int SalesTeamId { get; set; }

    public int? CompanyId { get; set; }

    public int? BranchId { get; set; }

    public int? DepartmentId { get; set; }

    public string? SalesTeamName { get; set; }

    public int? TeamLeaderId { get; set; }

    public int? IsActive { get; set; }
}
