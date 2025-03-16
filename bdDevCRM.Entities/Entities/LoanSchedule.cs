using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class LoanSchedule
{
    public int LoanScheduleId { get; set; }

    public int LoanId { get; set; }

    public DateTime? EmiDate { get; set; }

    public decimal? EmiAmount { get; set; }

    public decimal? PrincipalAmount { get; set; }

    public decimal? InterestAmount { get; set; }

    public int? StatusId { get; set; }

    public DateTime? PaidDate { get; set; }

    /// <summary>
    /// 1 = Paid else Unpaid
    /// </summary>
    public int? IsPaid { get; set; }

    public virtual LoanInformation Loan { get; set; } = null!;

    public virtual Wfstate? Status { get; set; }
}
