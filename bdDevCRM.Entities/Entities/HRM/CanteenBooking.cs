using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class CanteenBooking
{
    public int CanteenBookingId { get; set; }

    public int CanteenId { get; set; }

    public int BranchId { get; set; }

    public int Year { get; set; }

    public int Month { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<CanteenBookingMonthlyMeals> CanteenBookingMonthlyMeals { get; set; } = new List<CanteenBookingMonthlyMeals>();
}
