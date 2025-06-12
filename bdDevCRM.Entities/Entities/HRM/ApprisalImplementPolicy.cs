using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class ApprisalImplementPolicy
{
    public int ApproverRatingId { get; set; }

    public int? RattingSlabId { get; set; }

    public int? IsDoublePromotion { get; set; }

    public int? IsSinglePromotion { get; set; }

    public int? IsNormalIncrement { get; set; }

    public int? IsNoIncrement { get; set; }

    public int? IsIncrement { get; set; }

    public string? NumberOfIncrement { get; set; }

    public int? CtcIncrement { get; set; }

    public int? IsSpecialAllownce { get; set; }

    public string? NumberOfIncrementSpecial { get; set; }

    public int? CtcIncrementSpecial { get; set; }

    public int? IsActive { get; set; }
}
