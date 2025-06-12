using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class ApplicantPayroll
{
    public int ApplicantPayrollId { get; set; }

    public int ApplicantId { get; set; }

    public int? GradeId { get; set; }

    public decimal? GrossPay { get; set; }

    public decimal? CostToCompany { get; set; }

    public int? IsProvidedByCompany { get; set; }

    public decimal? NetTaxPayable { get; set; }

    public int? PayrollApprobedBy { get; set; }

    public DateTime? PayrollApproveDate { get; set; }

    public DateTime? PayrollEffectiveDate { get; set; }

    public int? Status { get; set; }

    public int? OmitTaxCalculation { get; set; }

    public int? IsHspitalizatonAvailable { get; set; }

    public decimal? MaxClaimedAmount { get; set; }

    public decimal? TaxProvidedByCompanyPer { get; set; }

    public decimal? TaxProvidedByEmployee { get; set; }
}
