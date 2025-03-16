using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class BgdManpowerRequisition
{
    public int ManpowerReqId { get; set; }

    public int CompanyId { get; set; }

    public int? BranchId { get; set; }

    public int? DivisionId { get; set; }

    public int? DepartmentId { get; set; }

    public int? FacilityId { get; set; }

    public int? EmployeeQty { get; set; }

    public int? FiscalYearId { get; set; }

    public int? GradeId { get; set; }

    public int? StateId { get; set; }

    public int? SavedBy { get; set; }

    public DateTime? SaveDate { get; set; }

    public int? DeletedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? SectionId { get; set; }

    public int? EmployeeTypeId { get; set; }

    public string? Remarks { get; set; }

    public bool? IsNeedReview { get; set; }

    public string? Attachment { get; set; }

    public int? HrFunctionId { get; set; }

    public int? IsBudgeted { get; set; }

    public int? IsNewRecruitment { get; set; }

    public int? IsReplacement { get; set; }

    public int? ManpowerGradeId { get; set; }

    public int? EmploymentId { get; set; }

    public int? EmployeeCategoryId { get; set; }
}
