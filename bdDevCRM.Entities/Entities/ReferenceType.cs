using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class ReferenceType
{
    public int ReferenceTypeId { get; set; }

    public string ReferenceTypeName { get; set; } = null!;
}
