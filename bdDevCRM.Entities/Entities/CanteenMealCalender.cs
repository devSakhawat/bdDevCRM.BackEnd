using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class CanteenMealCalender
{
    public int MealCalenderId { get; set; }

    public string MealCalenderName { get; set; } = null!;

    public string CutOffTime { get; set; } = null!;

    public int CanteenId { get; set; }

    public bool IsActive { get; set; }
}
