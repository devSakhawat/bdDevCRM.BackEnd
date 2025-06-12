using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class LoanScheduleAudit
{
    public int LoanScheduleAuditId { get; set; }

    public int? LoanScheduleId { get; set; }

    public int LoanId { get; set; }

    public DateTime? EmiDate { get; set; }

    public decimal? EmiAmount { get; set; }

    public decimal? PrincipalAmount { get; set; }

    public decimal? InterestAmount { get; set; }

    public int? StatusId { get; set; }

    public DateTime? PaidDate { get; set; }

    public int? IsPaid { get; set; }
}
