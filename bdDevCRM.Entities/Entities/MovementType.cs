using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class MovementType
{
    public int MovementTypeId { get; set; }

    public string MovementTypeName { get; set; } = null!;

    public string MovementTypeCode { get; set; } = null!;

    public int IsActiveMov { get; set; }
}
