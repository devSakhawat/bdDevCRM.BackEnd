using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class OvertimePaid
{
    public int OvertimeProcessId { get; set; }

    public int HrRecordId { get; set; }

    public DateOnly? OverTimeMonth { get; set; }

    public DateOnly? OverTimeFromDate { get; set; }

    public DateOnly? OverTimeToDate { get; set; }

    public decimal? GrossAmount { get; set; }

    public decimal? TaxAmount { get; set; }

    public decimal? NetPayout { get; set; }

    public int? IsProvidedByCompany { get; set; }

    public int? PaymentType { get; set; }

    public int? StateId { get; set; }

    public DateTime? GenerateDate { get; set; }

    public int? GenerateBy { get; set; }

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

    public decimal? ActualBasic { get; set; }

    public int? DivisionId { get; set; }

    public int? FacilityId { get; set; }

    public int? SectionId { get; set; }

    public int? FuncId { get; set; }

    public int? PaymentBy { get; set; }

    public DateTime? PaymentDate { get; set; }

    public int? CostCentreId { get; set; }

    public int? DesignationTypeId { get; set; }

    public string? RemarksForEdit { get; set; }
}
