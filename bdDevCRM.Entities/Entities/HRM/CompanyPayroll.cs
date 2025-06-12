using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class CompanyPayroll
{
    public int CompanyPayrollId { get; set; }

    public int? CompanyId { get; set; }

    /// <summary>
    /// 1=Salary, 2=Benifits
    /// </summary>
    public decimal? BasicSalary { get; set; }

    public decimal? MedicalAllowance { get; set; }

    public decimal? HouseRent { get; set; }

    /// <summary>
    /// 0=Nothing, 1=Addition with Salary, 2=Substruction from Salary
    /// </summary>
    public decimal? MobileAllowance { get; set; }

    public decimal? OtherAllowance { get; set; }

    public decimal? FestivalBonus { get; set; }

    public decimal? PerformanceBonus { get; set; }

    public decimal? ProjectBonus { get; set; }

    public decimal? ProfitSharing { get; set; }

    public decimal? CssfundEmployee { get; set; }

    public decimal? CssfundEmployer { get; set; }
}
