using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class RecSalaryMatchingInformation
{
    public int SalaryMatchId { get; set; }

    public int? ApplicantId { get; set; }

    public int? JobId { get; set; }

    public int? CurrentMonthlyCtc { get; set; }

    public int? ItsMaxSalary { get; set; }

    public int? ItsMinSalary { get; set; }

    public decimal? ItsAvrExp { get; set; }

    public decimal? ItsMinExp { get; set; }

    public decimal? ItsMaxExp { get; set; }

    public decimal? ExpectedSalary { get; set; }

    public decimal? CurrentMonthlyGross { get; set; }

    public decimal? ProposedMonthlyGross { get; set; }

    public int? ProposedMonthlyCtc { get; set; }

    public decimal? ChangeInMonthlyGross { get; set; }

    public int? CanJoinWithin { get; set; }

    public int? ItsAvgSalary { get; set; }

    public int? AddedBy { get; set; }

    public DateTime? AddedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public decimal? JoiningBonus { get; set; }

    public decimal? ChangeInMonthlyCtc { get; set; }

    public decimal? NegotiatedMonthlyGross { get; set; }

    public string? Comments { get; set; }

    public string? SalaryDescription { get; set; }
}
