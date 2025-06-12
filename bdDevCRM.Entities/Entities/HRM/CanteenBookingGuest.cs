using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class CanteenBookingGuest
{
    public int CanteenBookingGuestId { get; set; }

    public int BookingCalenderMonthlyId { get; set; }

    public string? GuestName { get; set; }

    public string? FromLocation { get; set; }

    public string? Purpose { get; set; }

    public int MealTypeId { get; set; }

    public int PayBy { get; set; }

    public int? SubsidiaryTypeId { get; set; }

    public virtual CanteenBookingMonthlyMeals BookingCalenderMonthly { get; set; } = null!;
}
