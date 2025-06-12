using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class OnsiteClientArchive2022
{
    public int OnsiteClientId { get; set; }

    public int? UserId { get; set; }

    public string? ClientName { get; set; }

    public string? ProjectCode { get; set; }

    public DateOnly? FromDate { get; set; }

    public DateOnly? ToDate { get; set; }

    public string? Remarks { get; set; }

    public DateTime? AppliedDate { get; set; }

    public int? Status { get; set; }

    public int? RecomanderId { get; set; }

    public DateTime? RecomanderDate { get; set; }

    public int? ApproverId { get; set; }

    public DateTime? ApproveDate { get; set; }

    public string? ClientCode { get; set; }

    public decimal? DayNo { get; set; }

    public int? PaymentId { get; set; }

    public decimal? ConvenceAmount { get; set; }

    public string? TransportationDescription { get; set; }

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

    public string? AttachmentPath { get; set; }
}
