using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class InvestmentInformation
{
    public int InvestmentId { get; set; }

    public double? MaxInvestmentAmount { get; set; }

    public double? PercentageOfTaxableIncome { get; set; }

    public double? BasicRebateRate { get; set; }

    public double? InvestmentExemptionRate { get; set; }

    public double? MinimumTaxAmount { get; set; }

    public int? SalaryYearId { get; set; }

    public int? AssesmentYearId { get; set; }

    public decimal? OldAgeRestriction { get; set; }

    public int? ConsiderDefaultRebate { get; set; }

    public int? ConsiderJoiningDate { get; set; }

    public int? ConsiderBasicRebate { get; set; }

    public int? ConsiderSalaryHistory { get; set; }

    public int? ConsiderPfProfit { get; set; }

    public decimal? LessTaxPfofBasic { get; set; }

    public decimal? OrPfInterest { get; set; }

    public decimal? ActualRateOfPfinterest { get; set; }

    public decimal? ConsidarableTaxableIncome { get; set; }

    public int? ConsiderTaxHistorySalary { get; set; }

    public int? IsOmitInvestmentCalculation { get; set; }

    public int? IsTotalExamptedonAnnualIncome { get; set; }

    public decimal? MaxExamptedAmountForTotalIncome { get; set; }

    public decimal? MaxExamptedPercentageForTotalIncome { get; set; }

    public int? ExamptionType { get; set; }
}
