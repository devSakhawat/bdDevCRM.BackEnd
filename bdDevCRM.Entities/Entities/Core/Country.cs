using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities.Core;

public partial class Country
{
    public int CountryId { get; set; }

    public string? CountryName { get; set; }

    public string? CountryCode { get; set; }

    public int? Status { get; set; }
}
