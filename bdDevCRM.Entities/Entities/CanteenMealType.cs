using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class CanteenMealType
{
    public int MealTypeId { get; set; }

    public string MealCode { get; set; } = null!;

    public string MealName { get; set; } = null!;

    public int TotalPrice { get; set; }

    public string SmsCode { get; set; } = null!;

    public bool IsActive { get; set; }
}
