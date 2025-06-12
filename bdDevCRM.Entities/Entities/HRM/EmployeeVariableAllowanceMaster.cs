using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class EmployeeVariableAllowanceMaster
{
    public int VariableAllowanceMasterId { get; set; }

    public DateTime? AllowanceFormDate { get; set; }

    public DateTime? AllowanceToDate { get; set; }

    public decimal? AvgAmount { get; set; }

    public int? SubmitBy { get; set; }

    public DateTime? SubmitDate { get; set; }

    public int? CompanyId { get; set; }

    public int? BranchId { get; set; }

    public int? DivisionId { get; set; }

    public int? DepartmentId { get; set; }

    public int? FacilityId { get; set; }

    public int? SectionId { get; set; }

    public int? FunctionId { get; set; }

    public int? AllowanceTypeId { get; set; }

    public int? StateId { get; set; }

    public int? ApproveBy { get; set; }

    public DateTime? ApproveDate { get; set; }

    public int? RejectBy { get; set; }

    public DateTime? RejectDate { get; set; }
}
