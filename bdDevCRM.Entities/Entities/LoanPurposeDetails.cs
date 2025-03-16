using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class LoanPurposeDetails
{
    public int LoanPurposeDetailsId { get; set; }

    public int LoanId { get; set; }

    public int LoanPurposeId { get; set; }
}
