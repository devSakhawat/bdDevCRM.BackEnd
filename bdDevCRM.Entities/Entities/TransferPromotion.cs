using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class TransferPromotion
{
    public int TransferPromotionId { get; set; }

    public int HrRecordId { get; set; }

    public int PostingTypeId { get; set; }

    public int CompanyId { get; set; }

    public int BranchId { get; set; }

    public int? DepartmentId { get; set; }

    public int? DesignationId { get; set; }

    public int? FunctionId { get; set; }

    public int EmployeeTypeId { get; set; }

    public int? ShiftId { get; set; }

    public int? GradeId { get; set; }

    public int? ReportDepId { get; set; }

    public int? ReportTo { get; set; }

    public string? Remarks { get; set; }

    public DateTime? EffectiveDate { get; set; }

    public DateTime? EvalutionDate { get; set; }

    public int? EvalutionBy { get; set; }

    public int? ApproverId { get; set; }

    public DateTime? ApproveDate { get; set; }

    public int StatusId { get; set; }

    public int? DivisionId { get; set; }

    public int? FacilityId { get; set; }

    public int? SectionId { get; set; }

    public int? Approver { get; set; }

    public int? SalaryLocation { get; set; }

    public int? ApproverDepartmentId { get; set; }

    public int? CostCentreId { get; set; }

    public DateTime? TransferLetterRaisedDate { get; set; }

    public string? NewEmpId { get; set; }

    public virtual PostingType PostingType { get; set; } = null!;
}
