using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class VoucharType
{
    public int VoucharTypeId { get; set; }

    public string VoucharTypeName { get; set; } = null!;

    public int? IsActive { get; set; }
}
