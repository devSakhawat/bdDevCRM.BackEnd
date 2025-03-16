using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class EmbassyInfo
{
    public int? EmbassyId { get; set; }

    public string? EmbassyName { get; set; }

    public string? EmbassyTitle { get; set; }

    public string? EmbassyAddress { get; set; }

    public string? ContryName { get; set; }

    public bool? IsActive { get; set; }
}
