using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class Religion
{
    public int ReligionId { get; set; }

    public string? ReligionName { get; set; }

    public string? RelegionCode { get; set; }

    public int? Status { get; set; }
}
