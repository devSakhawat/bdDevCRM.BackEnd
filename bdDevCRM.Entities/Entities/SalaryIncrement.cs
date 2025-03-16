using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class SalaryIncrement
{
    public int SalaryIncrementId { get; set; }

    public int HrRecordId { get; set; }

    public int? GradeId { get; set; }

    public decimal? GrossPay { get; set; }

    public decimal? CostToCompany { get; set; }

    public int? IsProvidedByCompany { get; set; }

    public decimal? NetTaxPayable { get; set; }

    public int? IncrementApprobedBy { get; set; }

    public DateTime? IncrementApproveDate { get; set; }

    public DateTime? IncrementEffectiveDate { get; set; }

    public int? Status { get; set; }

    public int? OmitTaxCalculation { get; set; }

    public int? IsHspitalizatonAvailable { get; set; }

    public decimal? MaxClaimedAmount { get; set; }

    public decimal? TaxProvidedByCompanyPer { get; set; }

    public decimal? TaxProvidedByEmployee { get; set; }

    public DateTime? IncrementMonth { get; set; }

    public int? IncrementTypeId { get; set; }

    public int? NewGradeId { get; set; }

    public decimal? NewGrossPayOnInc { get; set; }

    public decimal? NewCostToCompany { get; set; }

    public int? StateId { get; set; }

    public int? IncrementNo { get; set; }

    public int? CompanyId { get; set; }

    public int? BranchId { get; set; }

    public int? SalaryLocation { get; set; }

    public int? DivisionId { get; set; }

    public int? DepartmentId { get; set; }

    public int? DesignationId { get; set; }

    public int? EmployeeTypeId { get; set; }

    public int? FacilityId { get; set; }

    public int? SectionId { get; set; }

    public int? FuncId { get; set; }

    public decimal? SpecialAmount { get; set; }

    public int? IncrementGeneratedBy { get; set; }

    public DateTime? IncrementGeneratedDate { get; set; }
}
