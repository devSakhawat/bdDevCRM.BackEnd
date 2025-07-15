using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities.CRM;

public partial class Othersinformation
{
    public int OthersinformationId { get; set; }

    public int ApplicantId { get; set; }

    public string? OthersadditionalInformation { get; set; }

    public string? OthersscannedCopyPath { get; set; }

    public DateTime CreatedDate { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public virtual CrmApplication Applicant { get; set; } = null!;
}
