using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class EmployeeBank
{
    public int EmployeeBankId { get; set; }

    public int? EmployeeId { get; set; }

    public int? BankBranchId { get; set; }

    public string? BankAccountNo { get; set; }
}
