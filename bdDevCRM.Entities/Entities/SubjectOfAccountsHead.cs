using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class SubjectOfAccountsHead
{
    public int SubjectOfAccountHeadId { get; set; }

    public int? AccountHeadId { get; set; }

    public int? SubjectOfAccountId { get; set; }

    public int? Status { get; set; }
}
