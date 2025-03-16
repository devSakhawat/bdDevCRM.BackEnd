using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class RecJobIdSelectionDetails
{
    public int JobIdSelectionDetailsId { get; set; }

    public int? JobIdSelectionMasterId { get; set; }

    public int? JobId { get; set; }

    public int? State { get; set; }

    public int? IsProcessOpen { get; set; }

    public int? IsRejected { get; set; }

    public int? RejectedBy { get; set; }

    public DateTime? RejectedDate { get; set; }
}
