using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class CanteenMealTypeSubsidiary
{
    public int SubsidiaryId { get; set; }

    public int MealTypeId { get; set; }

    public int SubsidiaryTypeId { get; set; }

    public string? SubsidiaryTypeName { get; set; }

    public int EmployeeContributionAmount { get; set; }

    public bool IsActive { get; set; }
}
