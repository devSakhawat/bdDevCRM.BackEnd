using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class LeaveFormAndCompanyMapping
{
    public int LeaveFormAndCompanyMappingId { get; set; }

    public int? CompanyId { get; set; }

    public string? LeaveFormForSector { get; set; }
}
