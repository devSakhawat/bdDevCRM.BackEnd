using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class LeaveEncashmentReferenceNo
{
    public DateOnly? PaymentMonth { get; set; }

    public string? ReferenceNo { get; set; }

    public int? UniqueNumber { get; set; }
}
