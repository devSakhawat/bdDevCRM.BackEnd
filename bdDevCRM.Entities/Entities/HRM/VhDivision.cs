using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class VhDivision
{
    public int DivisionId { get; set; }

    public string? DivisionName { get; set; }

    public string? DivisionCode { get; set; }

    public int? IsActive { get; set; }
}
