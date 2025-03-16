using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class VhModel
{
    public int ModelId { get; set; }

    public string? ModelNo { get; set; }

    public string? ModelYear { get; set; }

    public int? IsActive { get; set; }
}
