using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class RecEducationalVerification
{
    public int EducationVerificationId { get; set; }

    public int? CertificateTypeId { get; set; }

    public string? CertificateTypeName { get; set; }

    public int? IsOriginalCopy { get; set; }

    public int? IsOnline { get; set; }

    public string? Others { get; set; }

    public int? ApplicantId { get; set; }
}
