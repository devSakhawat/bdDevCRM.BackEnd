using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class ConfirmationPolicyDetails
{
    public int ConfirmationPolicyDetailsId { get; set; }

    public int? ConfirmationPolicyId { get; set; }

    public int? GradeId { get; set; }
}
