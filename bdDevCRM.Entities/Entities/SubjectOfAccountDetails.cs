using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class SubjectOfAccountDetails
{
    public int SubjectOfAccountsDetailsId { get; set; }

    public int? SubjectOfAccountId { get; set; }

    public int? BankId { get; set; }

    public int? BranchId { get; set; }

    public string? AccountName { get; set; }

    public string? AccountNo { get; set; }

    public int? AccountTypeId { get; set; }

    public int? Status { get; set; }

    public int? CompanyId { get; set; }
}
