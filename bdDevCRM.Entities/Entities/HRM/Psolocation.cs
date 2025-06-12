using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class Psolocation
{
    public int PsolocationId { get; set; }

    public string? PsolocationCode { get; set; }

    public string? PsolocationName { get; set; }

    public string? DsmlocationCode { get; set; }

    public int? IsActive { get; set; }
}
