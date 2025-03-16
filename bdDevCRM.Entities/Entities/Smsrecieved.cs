using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class Smsrecieved
{
    public long Id { get; set; }

    public long? Smsindex { get; set; }

    public DateTime? RecievedDate { get; set; }

    public string? Smstext { get; set; }

    public string? FromMobileNumber { get; set; }

    public int? Status { get; set; }

    public DateTime? SystemDate { get; set; }

    public string? SimNumber { get; set; }

    public int? PartNo { get; set; }

    public int? PartMsgReference { get; set; }

    public int? TotalPart { get; set; }
}
