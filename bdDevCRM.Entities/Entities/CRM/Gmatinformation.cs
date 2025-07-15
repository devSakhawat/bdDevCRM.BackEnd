using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities.CRM;

public partial class Gmatinformation
{
    public int GmatinformationId { get; set; }

    public int ApplicantId { get; set; }

    public string? Gmatlistening { get; set; }

    public string? Gmatreading { get; set; }

    public string? Gmatwriting { get; set; }

    public string? Gmatspeaking { get; set; }

    public string? GmatoverallScore { get; set; }

    public DateTime? Gmatdate { get; set; }

    public string? GmatscannedCopyPath { get; set; }

    public string? GmatadditionalInformation { get; set; }

    public DateTime CreatedDate { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public virtual CrmApplication Applicant { get; set; } = null!;
}
