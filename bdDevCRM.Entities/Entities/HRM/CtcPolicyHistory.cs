using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class CtcPolicyHistory
{
    public int CtcPolicyHistoryId { get; set; }

    public int? SalaryYearId { get; set; }

    public int CtcId { get; set; }

    public string CtcCode { get; set; } = null!;

    public string CtcName { get; set; } = null!;

    public int IsExampted { get; set; }

    public int IsFullExampted { get; set; }

    public decimal? MaxExamptedAmt { get; set; }

    public int? ValueType { get; set; }

    public decimal CalculationValue { get; set; }

    public int? ParentCtc { get; set; }

    public int CtcType { get; set; }

    public int CtcOperator { get; set; }

    public int PayoutVisibility { get; set; }

    public int? CtcInstalment { get; set; }

    public int? AddToPf { get; set; }

    public int? MaturedPeriod { get; set; }

    public int SortOrder { get; set; }

    public int IsActive { get; set; }

    public int IsDefault { get; set; }

    public int? MakerId { get; set; }

    public DateTime? MakeDate { get; set; }

    public int? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int CtcCategory { get; set; }

    public int? TaxProvidedByCompany { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? HistoryUpdateBy { get; set; }

    public DateTime? HistoryUpdateDate { get; set; }

    public int? AddPfArear { get; set; }

    public int? IsIncomeTax { get; set; }

    public int? IsLfa { get; set; }

    public int? IsFestibleBonus { get; set; }

    public int? BonusEligibility { get; set; }

    public int? ProrataBonusPeriod { get; set; }

    public int? BonusCalculationBasis { get; set; }

    public int? BonusCutOfDate { get; set; }

    public int? ProvitionAccountHead { get; set; }

    public int? IsSingleAccount { get; set; }

    public int? IsPersonalUsed { get; set; }

    public int? CalculationValueForPersonalUsed { get; set; }

    public int? ParentCtcForPersonalUsed { get; set; }

    public decimal? FixedValueForPersonalUsed { get; set; }

    public int? PersonalUsedValueType { get; set; }

    public int? IsFestibleBonusProrataCalculateForTax { get; set; }

    public int? CtcInstalmentPerBonus { get; set; }

    public int? IsPercentageCalculationForExampted { get; set; }

    public int? IsDailyAllowance { get; set; }

    public int? IsWelfareFund { get; set; }

    public int? AllowanceType { get; set; }
}
