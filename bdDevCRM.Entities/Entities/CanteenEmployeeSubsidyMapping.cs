using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class CanteenEmployeeSubsidyMapping
{
    public int EmployeeSubsidyId { get; set; }

    public int SubsidiaryTypeId { get; set; }

    public int HrrecordId { get; set; }

    public int? CompanyId { get; set; }

    public int? BranchId { get; set; }
}
