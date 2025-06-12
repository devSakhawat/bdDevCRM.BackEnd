using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class Payroll
{
    public int PayrollId { get; set; }

    public int HrrecordId { get; set; }

    public string TinNumber { get; set; } = null!;

    public decimal CurrentGrossPay { get; set; }

    public decimal BasicofGross { get; set; }

    public decimal? MedicalAllowance { get; set; }

    public decimal? HouseRent { get; set; }

    public decimal? MobileAllowance { get; set; }

    public decimal? OtherAllowance { get; set; }

    public decimal? FestivalBonus { get; set; }

    public decimal? PerformanceBonus { get; set; }

    public decimal? ProjectBonus { get; set; }

    public decimal? ProfitSharing { get; set; }

    public decimal? CssfundEmployee { get; set; }

    public decimal? CssfundEmployer { get; set; }

    public decimal? HospitalInsurance { get; set; }

    public int? UserId { get; set; }

    public DateTime? LastUpdateDate { get; set; }

    public decimal? PfopeningBalance { get; set; }

    public int? Payrolltype { get; set; }

    public int? Gradeid { get; set; }

    public int? Otapplicable { get; set; }

    public decimal? Otrate { get; set; }

    public int? Wagestype { get; set; }

    public decimal? Workinghour { get; set; }

    public decimal? Rate { get; set; }

    public int? Stateid { get; set; }

    public DateTime? Activedate { get; set; }

    public DateTime? Approvedate { get; set; }

    public int? Authorizeby { get; set; }

    public DateTime? Nextreviewdate { get; set; }
}
