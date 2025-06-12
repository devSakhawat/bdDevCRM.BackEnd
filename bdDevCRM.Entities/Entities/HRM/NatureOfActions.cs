using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class NatureOfActions
{
    public int NatureOfActionId { get; set; }

    public string NatureOfActionName { get; set; } = null!;

    public int IsActive { get; set; }
}
