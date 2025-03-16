using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class CanteenMealCalenderWeekdayPlanning
{
    public int WeekDayPlanningId { get; set; }

    public int MealCalenderId { get; set; }

    public int MealId { get; set; }

    public int WeekdayNumber { get; set; }

    public bool IsActive { get; set; }

    public int CanteenId { get; set; }
}
