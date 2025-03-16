using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class CanteenGuestBillPay
{
    public int GuestBillPayId { get; set; }

    public string GuestBillPayBy { get; set; } = null!;
}
