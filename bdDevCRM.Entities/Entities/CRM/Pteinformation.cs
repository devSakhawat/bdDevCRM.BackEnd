using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities.CRM;

public partial class PTEInformation
{
    public int PTEInformationId { get; set; }

    public int ApplicantId { get; set; }

    public string? PTEListening { get; set; }

    public string? PTEReading { get; set; }

    public string? PTEWriting { get; set; }

    public string? PTESpeaking { get; set; }

    public string? PTEOverallScore { get; set; }

    public DateTime? PTEDate { get; set; }

    public string? PTEScannedCopyPath { get; set; }

    public string? PTEAdditionalInformation { get; set; }

    public DateTime CreatedDate { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public virtual CrmApplication Applicant { get; set; } = null!;
}
