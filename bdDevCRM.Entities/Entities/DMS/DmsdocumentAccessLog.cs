using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities.DMS;

public partial class DmsdocumentAccessLog
{
    public long LogId { get; set; }

    public int DocumentId { get; set; }

    public int AccessedByUserId { get; set; }

    public DateTime AccessDateTime { get; set; }

    public string Action { get; set; } = null!;

    public virtual Dmsdocument Document { get; set; } = null!;
}
