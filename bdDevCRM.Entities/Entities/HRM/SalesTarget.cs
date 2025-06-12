using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class SalesTarget
{
    public int SalesTargetId { get; set; }

    public int? HrRecordId { get; set; }

    public int? SalesTeamId { get; set; }

    public int? IsTeamLeader { get; set; }

    public int? CompanyId { get; set; }

    public int? BranchId { get; set; }

    public int? DepartmentId { get; set; }

    public int? MonthlyTarget { get; set; }

    public int? TargetMonth { get; set; }

    public int? TargetYear { get; set; }

    public DateOnly? CreateDate { get; set; }

    public int? CreatedBy { get; set; }

    public DateOnly? UpdateDate { get; set; }

    public int? UpdatedBy { get; set; }

    public int? ApproverId { get; set; }

    public DateOnly? ApproveDate { get; set; }

    public int? Status { get; set; }
}
