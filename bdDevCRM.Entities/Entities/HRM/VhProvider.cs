using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class VhProvider
{
    public int ProviderId { get; set; }

    public string? ProviderName { get; set; }

    public int? IsActive { get; set; }
}
