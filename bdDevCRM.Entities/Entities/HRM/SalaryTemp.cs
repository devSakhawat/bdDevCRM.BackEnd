using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class SalaryTemp
{
    public int? HrRecordId { get; set; }

    public DateTime? SalaryMonth { get; set; }

    public DateTime? GenerateDate { get; set; }

    public decimal? GrossPay { get; set; }

    public decimal? NetPayout { get; set; }

    public int? StateId { get; set; }

    public DateTime? LastUpdateDate { get; set; }

    public int? EmployeePayrollId { get; set; }

    public decimal? Tax { get; set; }

    public int? IsProvidedByCompany { get; set; }

    public int? GenerateBy { get; set; }

    public int? PaymentType { get; set; }

    public decimal? AdjustmentTax { get; set; }

    public int? CompanyId { get; set; }

    public int? BranchId { get; set; }

    public int? DepartmentId { get; set; }

    public int? DesignationId { get; set; }

    public int? EmployeeTypeId { get; set; }

    public string? BankAccountNo { get; set; }

    public int? GradeId { get; set; }

    public decimal? ActualBasic { get; set; }

    public int? SalaryType { get; set; }

    public int? DivisionId { get; set; }

    public int? FacilityId { get; set; }

    public int? SectionId { get; set; }

    public int? FuncId { get; set; }

    public int? DesignationTypeId { get; set; }

    public int? PaymentBy { get; set; }

    public DateTime? PaymentDate { get; set; }

    public int? IsBankPayment { get; set; }

    public int? BankPaymentBy { get; set; }

    public DateTime? BankPaymentDate { get; set; }

    public int? IsCashPayment { get; set; }

    public int? CashPaymentBy { get; set; }

    public DateTime? CashPaymentDate { get; set; }
}
