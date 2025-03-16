using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class MovementPolicyWithSbulist
{
    public int Id { get; set; }

    public int MovementPolicyId { get; set; }

    public int CompanyId { get; set; }
}
