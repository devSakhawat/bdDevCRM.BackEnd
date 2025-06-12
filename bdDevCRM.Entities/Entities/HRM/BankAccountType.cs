using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class BankAccountType
{
    public int AccountTypeId { get; set; }

    public string? AccountTypeName { get; set; }

    public int? IsActive { get; set; }
}
