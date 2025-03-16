using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class SimNumberSettings
{
    public int PhoneSettingsId { get; set; }

    public string? PhoneNumber { get; set; }

    public int? IsActive { get; set; }
}
