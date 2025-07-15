using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities.CRM;

public partial class Ieltsinformation
{
    public int IeltsinformationId { get; set; }

    public int ApplicantId { get; set; }

    public string? Ieltslistening { get; set; }

    public string? Ieltsreading { get; set; }

    public string? Ieltswriting { get; set; }

    public string? Ieltsspeaking { get; set; }

    public string? IeltsoverallScore { get; set; }

    public DateTime? Ieltsdate { get; set; }

    public string? IeltsscannedCopyPath { get; set; }

    public string? IeltsadditionalInformation { get; set; }

    public DateTime CreatedDate { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public virtual CrmApplication Applicant { get; set; } = null!;
}
