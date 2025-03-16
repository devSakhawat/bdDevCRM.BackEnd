using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class AttendanceAdjustment
{
    public int AdjustmentRequestId { get; set; }

    public int? UserId { get; set; }

    public DateTime? AttendanceDate { get; set; }

    public string Reason { get; set; } = null!;

    public string? Explanation { get; set; }

    public DateTime? AppliedDate { get; set; }

    public int? RecommanderId { get; set; }

    public DateTime? RecommandDate { get; set; }

    public int? ApproverId { get; set; }

    public DateTime? ApproveDate { get; set; }

    public int? StateId { get; set; }

    public int? CompanyId { get; set; }

    public int? BranchId { get; set; }

    public int? DivisionId { get; set; }

    public int? DepartmentId { get; set; }

    public int? FacilityId { get; set; }

    public int? SectionId { get; set; }

    public int? DesignationId { get; set; }

    public int? GradeId { get; set; }

    public int? EmployeeTypeId { get; set; }

    public int? FuncId { get; set; }
}
