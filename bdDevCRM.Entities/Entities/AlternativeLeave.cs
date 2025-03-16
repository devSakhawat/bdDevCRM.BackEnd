using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class AlternativeLeave
{
    public int AlternativeDayId { get; set; }

    public int? CompanyId { get; set; }

    public int? BranchId { get; set; }

    public int? DepartmentId { get; set; }

    public int? DesignationId { get; set; }

    public int? FacilityId { get; set; }

    public int? SectionId { get; set; }

    public int? SubSectionId { get; set; }

    public int? EmployeeTypeId { get; set; }

    public int? GradeId { get; set; }

    public int? DivisionId { get; set; }

    public int? HrRecordId { get; set; }

    public int? LeaveTypeId { get; set; }

    public DateOnly? OffDay { get; set; }

    public string? Remarks { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? IsApplied { get; set; }
}
