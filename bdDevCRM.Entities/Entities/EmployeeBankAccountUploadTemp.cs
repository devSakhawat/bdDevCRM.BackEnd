using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class EmployeeBankAccountUploadTemp
{
    public string? EmployeeId { get; set; }

    public string? BankName { get; set; }

    public string? BranchName { get; set; }

    public string? BankAccountNo { get; set; }

    public int? IsActive { get; set; }
}
