using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class Smssent
{
    public long Id { get; set; }

    public string? Smstext { get; set; }

    public string? MobileNumber { get; set; }

    public DateTime? RequestDateTime { get; set; }

    public DateTime? DeliveryDateTime { get; set; }

    public int? Status { get; set; }

    public long? ReplyFor { get; set; }

    public string? SimNumber { get; set; }

    public int? NoOfTry { get; set; }

    public int? MessageReference { get; set; }
}
