using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class PromotionGuideLine
{
    public int PromotionGuideLineId { get; set; }

    public int? RewardPenaltyGuideLineId { get; set; }

    public int? GradeLevelId { get; set; }

    public int? EmployeeTypeId { get; set; }

    public int? PreviousRequiredYearForConfirm { get; set; }

    public int? RequiredYearForConfirm { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? GradeLevelCrossingTypeId { get; set; }

    public int? GradeLevelCrossingRequireYear { get; set; }
}
