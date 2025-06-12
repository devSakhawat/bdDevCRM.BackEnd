using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class LedgerHead
{
    public int LedgerHeadId { get; set; }

    public string? LedgerHeadName { get; set; }

    public string? MappingTable { get; set; }

    public int? SubjectId { get; set; }
}
