using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class OtherPayment
{
    public int OtherPaymentId { get; set; }

    public int? HrRecordId { get; set; }

    public int? CtcId { get; set; }

    public decimal? CtcValue { get; set; }

    public decimal? TaxAmount { get; set; }

    public decimal? NetPayout { get; set; }

    public DateTime? PaymentMonth { get; set; }

    public int? GenerateBy { get; set; }

    public DateTime? GenerateDate { get; set; }

    public int? ApproveBy { get; set; }

    public DateTime? ApproveDate { get; set; }

    public int? PaymentBy { get; set; }

    public DateTime? PaymentDate { get; set; }

    public int? StatusId { get; set; }

    public int? AgeLimit { get; set; }

    public int? InstructionBankId { get; set; }

    public int? InstructionBranchId { get; set; }

    public string? InstructionAccountNo { get; set; }

    public int? CompanyId { get; set; }

    public int? BranchId { get; set; }

    public int? DepartmentId { get; set; }

    public int? DesignationId { get; set; }

    public int? EmployeeTypeId { get; set; }

    public string? BankAccountNo { get; set; }

    public int? GradeId { get; set; }

    public int? DivisionId { get; set; }

    public int? FacilityId { get; set; }

    public int? SectionId { get; set; }

    public int? FuncId { get; set; }

    public decimal? StampCharge { get; set; }

    public string? BonusPolicyName { get; set; }

    public string? Remarks { get; set; }

    public DateTime? FestivalDate { get; set; }

    public int? CostCentreId { get; set; }

    public DateTime? PaymentConfirmationDate { get; set; }
}
