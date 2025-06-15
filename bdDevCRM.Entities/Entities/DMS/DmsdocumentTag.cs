using bdDevCRM.Entities.Entities.DMS;
using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities.DMS;

public partial class DmsdocumentTag
{
    public int TagId { get; set; }

    public string DocumentTagName { get; set; } = null!;

    public virtual ICollection<Dmsdocument> Document { get; set; } = new List<Dmsdocument>();
}
