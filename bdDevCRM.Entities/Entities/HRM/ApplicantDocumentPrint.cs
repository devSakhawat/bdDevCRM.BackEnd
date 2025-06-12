using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class ApplicantDocumentPrint
{
    public int ApplicantDocumentId { get; set; }

    public int? ApplicantId { get; set; }

    public int? DocumentId { get; set; }

    public int? ParameterId { get; set; }

    public string? ParamValue { get; set; }

    public string? ParamIdentity { get; set; }
}
