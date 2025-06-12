using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class RewardDistribution
{
    public int RewardDistributionId { get; set; }

    public DateTime? DistributionDate { get; set; }

    public int? IsCoverLetterPrint { get; set; }

    public DateOnly? CoverLetterPrintDate { get; set; }

    public int? IsEnvelopePrint { get; set; }

    public DateOnly? EnvelopePrintDate { get; set; }

    public int? UserEmpId { get; set; }

    public int? LineManagerHrRecordId { get; set; }

    public int? DocumentTemplId { get; set; }

    public int? HrManagementHrRecordId { get; set; }
}
