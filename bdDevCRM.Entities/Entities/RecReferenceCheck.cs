using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class RecReferenceCheck
{
    public int ReferenceCheckId { get; set; }

    public int? ReferenceId { get; set; }

    public int? ApplicantId { get; set; }

    public int? Honesty { get; set; }

    public int? FinancialClearance { get; set; }

    public int? Professionalism { get; set; }

    public int? AnyComments { get; set; }
}
