using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities.DMS;

public partial class DmsdocumentTag
{
    public int TagId { get; set; }

    public string DocumentTagName { get; set; } = null!;

    public virtual ICollection<DmsdocumentTagMap> DmsdocumentTagMap { get; set; } = new List<DmsdocumentTagMap>();
}
