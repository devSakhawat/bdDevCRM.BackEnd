using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class OvertimeDeduction
{
    public int OvertimeDeductionId { get; set; }

    public int? HrRecordId { get; set; }

    public int? CompanyId { get; set; }

    public int? BranchId { get; set; }

    public int? DivisionId { get; set; }

    public int? DepartmentId { get; set; }

    public int? DesignationId { get; set; }

    public int? EmployeeTypeId { get; set; }

    public int? SectionId { get; set; }

    public int? FacilityId { get; set; }

    public int? FunctionId { get; set; }

    public DateOnly? FromDate { get; set; }

    public DateOnly? ToDate { get; set; }

    public decimal? Amount { get; set; }

    public string? Remarks { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? StateId { get; set; }

    public int? ApproveBy { get; set; }

    public DateOnly? ApproveDate { get; set; }

    public int? RejectBy { get; set; }

    public DateTime? RejectDate { get; set; }
}
