using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class CareerHistory
{
    public int EmploymentHistoryId { get; set; }

    public int HrrecordId { get; set; }

    public int CompanyId { get; set; }

    public int BranchId { get; set; }

    public int DepartmentId { get; set; }

    public int DesignationId { get; set; }

    public int FunctionId { get; set; }

    public int? EmployeeTypeId { get; set; }

    public int? ShiftId { get; set; }

    public int? GradeId { get; set; }

    public int? ReportDepId { get; set; }

    public int? ReportTo { get; set; }

    public string? Remarks { get; set; }

    public DateTime? EffectiveEndDate { get; set; }

    public DateTime? EffectiveStartDate { get; set; }

    public int? TransferPromotionId { get; set; }

    public int? PostingType { get; set; }

    public decimal? CurrentBasic { get; set; }

    public int? DivisionId { get; set; }

    public int? FacilityId { get; set; }

    public int? SectionId { get; set; }

    public int? ApproverDepartmentId { get; set; }

    public int? Approver { get; set; }

    public int? SalaryLocation { get; set; }

    public int? CostCentreId { get; set; }

    public string? EmpId { get; set; }
}
