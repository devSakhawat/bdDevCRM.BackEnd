using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class PayrollHistory
{
    public int PayrollHistoryId { get; set; }

    public int HrrecordId { get; set; }

    public decimal Currentgrosspay { get; set; }

    public DateTime Activedate { get; set; }

    public DateTime Lastupdatedate { get; set; }

    public DateTime Nextreviewdate { get; set; }

    public int? Payrollid { get; set; }

    public string? Tinnumber { get; set; }

    public decimal? Basicofgross { get; set; }

    public decimal? Medicalallowance { get; set; }

    public decimal? Houserent { get; set; }

    public decimal? Mobileallowance { get; set; }

    public decimal? Otherallowance { get; set; }

    public decimal? Festivalbonus { get; set; }

    public decimal? Performancebonus { get; set; }

    public decimal? Projectbonus { get; set; }

    public decimal? Profitsharing { get; set; }

    public decimal? Cssfundemployee { get; set; }

    public decimal? Cssfundemployer { get; set; }

    public decimal? Hospitalinsurance { get; set; }

    public decimal? Pfopeningbalance { get; set; }

    public int? Payrolltype { get; set; }

    public int? Gradeid { get; set; }

    public int? Otapplicable { get; set; }

    public decimal? Otrate { get; set; }

    public int? Wagestype { get; set; }

    public decimal? Workinghour { get; set; }

    public decimal? Rate { get; set; }

    public int? Stateid { get; set; }

    public DateTime? Approvedate { get; set; }

    public int? Authorizeby { get; set; }
}
