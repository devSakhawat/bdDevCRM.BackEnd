using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class MobileBillingTmp
{
    public int MobileBillingId { get; set; }

    public string? MobileNo { get; set; }

    public string? NwdAmount { get; set; }

    public decimal? IsdAmount { get; set; }

    public decimal? VasAmount { get; set; }

    public decimal? SmsAmount { get; set; }

    public decimal? InternetAmount { get; set; }

    public DateOnly? BillingMonth { get; set; }

    public int? UserId { get; set; }

    public DateOnly? SalaryDate { get; set; }

    public int? SimVendorId { get; set; }

    public decimal? TotalAmount { get; set; }

    public int? BillingStatus { get; set; }

    public decimal? TotalDeduction { get; set; }

    public int? CompanyId { get; set; }

    public int? BranchId { get; set; }

    public int? DivisionId { get; set; }

    public int? DepartmentId { get; set; }

    public int? FacilityId { get; set; }

    public int? SectionId { get; set; }

    public int? DesignationId { get; set; }

    public int? GradeId { get; set; }

    public int? EmployeeTypeId { get; set; }

    public int? FuncId { get; set; }

    public int? HrRecordId { get; set; }

    public decimal? AdjustmentAmount { get; set; }

    public int? AdustedBy { get; set; }

    public DateTime? AdjustedDate { get; set; }
}
