using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class WelfareFundApplication
{
    public int WelfareApplicationId { get; set; }

    public int? HrRecordId { get; set; }

    public DateOnly? ApplyDate { get; set; }

    public decimal? AppliedAmount { get; set; }

    public int? CompanyId { get; set; }

    public int? BranchId { get; set; }

    public int? DivisionId { get; set; }

    public int? DepartmentId { get; set; }

    public int? DesignationId { get; set; }

    public int? FacilityId { get; set; }

    public int? SectionId { get; set; }

    public int? FunctionId { get; set; }

    public int? GradeId { get; set; }

    public int? StateId { get; set; }

    public int? RecommanderId { get; set; }

    public DateOnly? RecommandDate { get; set; }

    public int? ApproverId { get; set; }

    public DateOnly? ApproveDate { get; set; }

    public string? Reason { get; set; }
}
