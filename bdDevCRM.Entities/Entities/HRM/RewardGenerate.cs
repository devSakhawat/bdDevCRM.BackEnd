using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class RewardGenerate
{
    public int RewardGenerateId { get; set; }

    public string? NatureOfReward { get; set; }

    public DateTime? RewardGenerateDate { get; set; }

    public int? IsCoverLetterPrint { get; set; }

    public DateOnly? CoverLetterPrintDate { get; set; }

    public int? IsEnvelopePrint { get; set; }

    public DateOnly? EnvelopePrintDate { get; set; }

    public int? HrManagementHrRecordId { get; set; }

    public int? ServiceLength { get; set; }

    public int? UserEmpId { get; set; }

    public int? LineManagerHrRecordId { get; set; }

    public int? RewardEligibilityId { get; set; }

    public DateOnly? RewardDate { get; set; }

    public int? DocumentTemplId { get; set; }
}
