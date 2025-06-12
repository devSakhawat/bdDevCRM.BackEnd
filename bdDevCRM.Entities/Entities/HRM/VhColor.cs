using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class VhColor
{
    public int ColorId { get; set; }

    public string? ColorName { get; set; }

    public int? IsActive { get; set; }
}
