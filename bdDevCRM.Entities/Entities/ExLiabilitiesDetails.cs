using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class ExLiabilitiesDetails
{
    public int LiabilitiesId { get; set; }

    public int? ClearanceSetupMasterId { get; set; }

    public int? ApplicationId { get; set; }

    public string? Description { get; set; }

    public decimal? Amount { get; set; }

    public int? IsLiabilities { get; set; }
}
