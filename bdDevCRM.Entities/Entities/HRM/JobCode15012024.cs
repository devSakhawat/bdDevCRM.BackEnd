using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class JobCode15012024
{
    public int JobId { get; set; }

    public string? CognosCode { get; set; }

    public int JobNumber { get; set; }

    public string Code { get; set; } = null!;

    public int? CompanyId { get; set; }

    public int? BranchId { get; set; }

    public int? DivisionId { get; set; }

    public int? DepartmentId { get; set; }

    public int? SectionId { get; set; }

    public int? DesignationId { get; set; }

    public int? IsOld { get; set; }

    public int? FacilityId { get; set; }

    public int? FuncId { get; set; }

    public int? ManpowerReqId { get; set; }

    public int? SalaryLocation { get; set; }

    public int? IsRejected { get; set; }

    public int? ManpowerGradeMapId { get; set; }
}
