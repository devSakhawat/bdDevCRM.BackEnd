using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities.CRM;

public partial class Toeflinformation
{
    public int ToeflinformationId { get; set; }

    public int ApplicantId { get; set; }

    public string? Toefllistening { get; set; }

    public string? Toeflreading { get; set; }

    public string? Toeflwriting { get; set; }

    public string? Toeflspeaking { get; set; }

    public string? ToefloverallScore { get; set; }

    public DateTime? Toefldate { get; set; }

    public string? ToeflscannedCopyPath { get; set; }

    public string? ToefladditionalInformation { get; set; }

    public DateTime CreatedDate { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public virtual CrmApplication Applicant { get; set; } = null!;
}
