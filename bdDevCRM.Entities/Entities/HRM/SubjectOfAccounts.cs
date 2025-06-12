using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class SubjectOfAccounts
{
    public int SubjectOfAccountId { get; set; }

    public string? SubjectOfAccountName { get; set; }

    public int? CtcId { get; set; }

    public decimal? MaximumLoanRate { get; set; }

    public int? DeafultEmpContAccountHead { get; set; }

    public int? DeafultCmpContAccountHead { get; set; }

    public int? IsActive { get; set; }

    public int? DefaultIncomeOwnAcHead { get; set; }

    public int? DefaultIncomeCompanyAcHead { get; set; }

    public int? DeafultOutgoingCmpContAccountHead { get; set; }

    public int? DefaultOutgoingIncomeOwnAcHead { get; set; }

    public int? DefaultOutgoingIncomeCompanyAcHead { get; set; }

    public int? DefaultPfInterestAccountHeadId { get; set; }

    public int? DefaultPfPrincipleAccountHeadId { get; set; }
}
