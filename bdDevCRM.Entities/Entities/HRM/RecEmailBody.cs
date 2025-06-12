using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class RecEmailBody
{
    public int EmailBodyId { get; set; }

    public string? EmailBodyName { get; set; }

    public string? EmailBody { get; set; }
}
