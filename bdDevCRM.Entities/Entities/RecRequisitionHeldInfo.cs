using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class RecRequisitionHeldInfo
{
    public int HoldId { get; set; }

    public int? JobIdSelectionMasterId { get; set; }

    public int? JobId { get; set; }

    public DateTime? HoldDate { get; set; }

    public DateTime? ReleaseDate { get; set; }

    public int? SavedBy { get; set; }

    public DateTime? SavedDate { get; set; }

    public int? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }
}
