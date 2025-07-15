using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities.CRM;

public partial class StatementOfPurpose
{
    public int StatementOfPurposeId { get; set; }

    public int ApplicantId { get; set; }

    public string? StatementOfPurposeRemarks { get; set; }

    public string? StatementOfPurposeFilePath { get; set; }

    public DateTime CreatedDate { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public virtual CrmApplication Applicant { get; set; } = null!;
}
