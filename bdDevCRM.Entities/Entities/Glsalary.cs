using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class Glsalary
{
    public int GlsalaryId { get; set; }

    public int? TranId { get; set; }

    public string? Sjvcode { get; set; }

    public short? Sjvtype { get; set; }

    public short? GltransferType { get; set; }

    public string? Sjvdesc { get; set; }

    public int? GlcompanyId { get; set; }

    public int? BankId { get; set; }

    public int? BankBranchId { get; set; }
}
