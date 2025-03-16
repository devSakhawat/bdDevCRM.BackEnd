using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class BgdManpowerReqGradeMap
{
    public int ManpowerGradeMapId { get; set; }

    public int ManpowerReqId { get; set; }

    public int? GradeId { get; set; }

    public int? DesignationId { get; set; }

    public int? EmployeeQuantity { get; set; }

    public DateOnly? ExpectedJoiningDate { get; set; }

    public int? AverageCtc { get; set; }

    public int? BranchId { get; set; }

    public int? DepartmentId { get; set; }

    public int? SalaryLocation { get; set; }

    public string? Remarks { get; set; }

    public int? NumberofExistingWorkforce { get; set; }

    public int? DivisionId { get; set; }
}
