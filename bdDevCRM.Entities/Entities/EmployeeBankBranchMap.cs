using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class EmployeeBankBranchMap
{
    public int EmployeeBankBranchMapId { get; set; }

    public int? HrrecordId { get; set; }

    public int? BranchId { get; set; }

    public string? BankAccountNo { get; set; }

    public int? IsActive { get; set; }
}
