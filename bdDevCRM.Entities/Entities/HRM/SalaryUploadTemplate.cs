using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class SalaryUploadTemplate
{
    public string? EmpId { get; set; }

    public DateOnly? SalaryMonth { get; set; }

    public decimal? Consolidated { get; set; }

    public decimal? Basic { get; set; }

    public decimal? HouseRent { get; set; }

    public decimal? Medical { get; set; }

    public decimal? Conveyance { get; set; }

    public decimal? Washing { get; set; }

    public decimal? HairCut { get; set; }

    public decimal? FoodAllow { get; set; }

    public decimal? BodyGuardAllow { get; set; }

    public decimal? VipdutyAllow { get; set; }

    public decimal? ChairmanAllow { get; set; }

    public decimal? Special { get; set; }

    public decimal? Arear { get; set; }

    public decimal? Gross { get; set; }

    public decimal? Pfded { get; set; }

    public decimal? AbsentAmt { get; set; }

    public decimal? LoanInstall { get; set; }

    public decimal? MotorCycleDed { get; set; }

    public decimal? Tax { get; set; }

    public decimal? TransportDed { get; set; }

    public decimal? FoodDed { get; set; }

    public decimal? MobileDed { get; set; }

    public decimal? SalaryAdv { get; set; }

    public decimal? OtherDed { get; set; }

    public decimal? TaxReturn { get; set; }

    public decimal? NetPay { get; set; }
}
