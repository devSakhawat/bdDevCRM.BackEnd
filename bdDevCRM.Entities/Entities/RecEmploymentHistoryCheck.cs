using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class RecEmploymentHistoryCheck
{
    public int EmploymentHistoryCheckId { get; set; }

    public int? ReferenceId { get; set; }

    public int? ApplicantId { get; set; }

    public int? VerificationTypeId { get; set; }

    public string? Remarks { get; set; }

    public int? UpdateBy { get; set; }

    public string? UpdateDate { get; set; }
}
