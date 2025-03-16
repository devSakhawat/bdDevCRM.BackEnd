using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class MovementLog
{
    public int MovementId { get; set; }

    public int? UserId { get; set; }

    public DateTime? MovementDate { get; set; }

    public string? OutTime { get; set; }

    public string? InTime { get; set; }

    public string? ExpectedReturnTime { get; set; }

    public int? Status { get; set; }

    public string? Remarks { get; set; }

    public string? ClientName { get; set; }

    public string? ProjectCode { get; set; }

    public decimal? ConvenceAmount { get; set; }

    public string? TransportationDescription { get; set; }

    public bool? IsApproved { get; set; }

    public bool? IsBackToOffice { get; set; }

    public int? ApproverId { get; set; }

    public DateTime? ApproveDate { get; set; }

    public int? RecomanderId { get; set; }

    public DateTime? RecomanderDate { get; set; }

    public int? StateId { get; set; }

    public int? MovementType { get; set; }

    public int? PaymentId { get; set; }

    public string? ClientCode { get; set; }

    public bool? IsMovementCancel { get; set; }

    public DateTime? ActualMovement { get; set; }

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

    public int? ShortLeaveSlot { get; set; }

    public DateTime? AppliedDateTime { get; set; }
}
