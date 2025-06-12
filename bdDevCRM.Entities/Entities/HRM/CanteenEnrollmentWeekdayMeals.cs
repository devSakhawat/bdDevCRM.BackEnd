using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class CanteenEnrollmentWeekdayMeals
{
    public int EnrollmentWeekdayMealId { get; set; }

    public int EnrollmentId { get; set; }

    public int MealId { get; set; }

    public int WeekdayNumber { get; set; }

    public int? NumberOfMeal { get; set; }

    public bool? IsActive { get; set; }
}
