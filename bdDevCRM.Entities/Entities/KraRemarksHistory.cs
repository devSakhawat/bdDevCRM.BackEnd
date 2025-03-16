using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class KraRemarksHistory
{
    public int KraRemarksId { get; set; }

    public int? KraPerformanceDetailId { get; set; }

    public string? Remarks { get; set; }

    public DateTime? RemarksDate { get; set; }

    public int? RemarksBy { get; set; }

    public int? StatusId { get; set; }
}
