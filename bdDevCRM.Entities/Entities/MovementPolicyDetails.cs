using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class MovementPolicyDetails
{
    public int DetailsId { get; set; }

    public int? MovementPolicyId { get; set; }

    public int? MovementType { get; set; }

    public decimal Duration { get; set; }
}
