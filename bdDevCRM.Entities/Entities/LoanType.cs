using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class LoanType
{
    public int LoanTypeId { get; set; }

    public string LoanTypeName { get; set; } = null!;

    /// <summary>
    /// 1= PF Loan else 0
    /// </summary>
    public int? IsPfloan { get; set; }

    public int? DebitAccountHeadId { get; set; }

    public int? CreditAccountHeadId { get; set; }

    public int? InterestRecvHeadId { get; set; }

    public int? Status { get; set; }

    public decimal? MaximumLoanAmount { get; set; }

    public int? TotalEmi { get; set; }

    public decimal? EmiAmountPer { get; set; }

    public string? TermsAndCondition { get; set; }

    public bool? LoanPurposeIsText { get; set; }

    public bool? EarlySettaledIncludingInterst { get; set; }

    public decimal? DefaultInterstRate { get; set; }

    public int? AccruedInterestHeadId { get; set; }

    public decimal? EmiAmount { get; set; }

    public int? IsJobLength { get; set; }

    public int? JobLength { get; set; }

    public int? EligibleAmountOn { get; set; }

    public int? IsMultipleLoan { get; set; }

    public int? MappingCtcId { get; set; }

    public int? ActualCtcId { get; set; }

    public int? SubjectOfAccountId { get; set; }

    public virtual ICollection<LoanInformation> LoanInformation { get; set; } = new List<LoanInformation>();
}
