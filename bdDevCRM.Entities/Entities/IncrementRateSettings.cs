using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class IncrementRateSettings
{
    public int IncrementRateId { get; set; }

    public int? GradeId { get; set; }

    public decimal? GradeWiseStartingBasic { get; set; }

    public decimal? GradeWiseIncrementRate { get; set; }

    public int? OtherAllowenceOnCtc { get; set; }

    public decimal? OtherAllowenceAmount { get; set; }

    public int? OtherAllowenceExcedCtc { get; set; }

    public int? IncrementAppliedOnBasic { get; set; }

    public int? RestIncrementCtc { get; set; }

    public int? MaxIncrementNo { get; set; }

    public int? MaxIncrementNoPerSession { get; set; }

    public decimal? OtherAllowanceGrossLimit { get; set; }

    public int? OtherAllowanceExceedCtc { get; set; }

    public int? IsFullIncrementAppliedOnBasic { get; set; }
}
