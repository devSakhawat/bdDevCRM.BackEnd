using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class CanteenBookingMonthlyMeals
{
    public int BookingCalenderMonthlyId { get; set; }

    public int CanteenBookingId { get; set; }

    public DateTime MealDate { get; set; }

    public string? ChangeType { get; set; }

    public int MealTypeId { get; set; }

    public int? EnrollmentId { get; set; }

    public int? CanteenId { get; set; }

    public int? BranchId { get; set; }

    public bool IsParent { get; set; }

    public bool IsActive { get; set; }

    public int? ParentId { get; set; }

    public int HrrecordId { get; set; }

    public int? IsNotClickable { get; set; }

    public virtual CanteenBooking CanteenBooking { get; set; } = null!;

    public virtual ICollection<CanteenBookingGuest> CanteenBookingGuest { get; set; } = new List<CanteenBookingGuest>();
}
