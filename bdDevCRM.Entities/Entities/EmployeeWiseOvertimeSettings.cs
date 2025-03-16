using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class EmployeeWiseOvertimeSettings
{
    public int OtsettingsId { get; set; }

    public int HrRecordId { get; set; }

    public int? CompanyId { get; set; }

    public int? BranchId { get; set; }

    public int? DivisionId { get; set; }

    public int? DepartmentId { get; set; }

    public int? FacilityId { get; set; }

    public int? SectionId { get; set; }

    public int? DesignationId { get; set; }

    public string? EmployeeType { get; set; }

    public int? FuncId { get; set; }

    public int? GradeId { get; set; }

    public decimal? Otamount { get; set; }

    public DateTime? EffectiveDate { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? IsActive { get; set; }

    public int? EmployeeTypeId { get; set; }
}
