using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class OtAllocation2019
{
    public int OtAllocationId { get; set; }

    public int CompanyId { get; set; }

    public int BranchId { get; set; }

    public int? DepartmentId { get; set; }

    public DateOnly OtFromDate { get; set; }

    public DateOnly OttoDate { get; set; }

    public decimal? AverageOtHour { get; set; }

    public int? IsActive { get; set; }

    public int? OtAllocationStart { get; set; }

    public string? Remarks { get; set; }

    public int? StateId { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? StateChangeBy { get; set; }

    public DateTime? StateChangeDate { get; set; }

    public int? DivisionId { get; set; }

    public int? FacilityId { get; set; }

    public int? SectionId { get; set; }

    public int? FunctionId { get; set; }

    public int? EmployeeTypeId { get; set; }

    public TimeOnly? OtTimeFrom { get; set; }

    public TimeOnly? OtTimeTo { get; set; }

    public int? HasBreakUp { get; set; }
}
