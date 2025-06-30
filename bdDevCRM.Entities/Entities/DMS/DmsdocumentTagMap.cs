using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities.DMS;

public partial class DmsdocumentTagMap
{
  public int TagMapId { get; set; }
  public int DocumentId { get; set; }

    public int TagId { get; set; }

    public virtual Dmsdocument Document { get; set; } = null!;

    public virtual DmsdocumentTag Tag { get; set; } = null!;
}
