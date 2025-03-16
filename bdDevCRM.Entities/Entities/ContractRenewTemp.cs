using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class ContractRenewTemp
{
    public int ContractRenewTempId { get; set; }

    public string EmployeeId { get; set; } = null!;

    public DateOnly? ContactStartDate { get; set; }

    public DateOnly? ContactEndDate { get; set; }

    public int? UserId { get; set; }
}
