using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class EmployeeVerificationForm
{
    public int EmployeeVerificationFormId { get; set; }

    public int? ApplicantId { get; set; }

    public int? Nidverification { get; set; }

    public int? PoliceVerification { get; set; }

    public int? Status { get; set; }
}
