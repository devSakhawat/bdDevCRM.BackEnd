using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class LoanPurpose
{
    public int LoanPurposeId { get; set; }

    public string PurposeName { get; set; } = null!;

    public int LoanType { get; set; }

    public bool? IsActive { get; set; }
}
