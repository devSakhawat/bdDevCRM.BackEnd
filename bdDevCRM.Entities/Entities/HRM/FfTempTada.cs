using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class FfTempTada
{
    public int Tadaid { get; set; }

    public string? EmployeeId { get; set; }

    public decimal? TotalDays { get; set; }

    public string? IsCurrentTerritory { get; set; }

    public int? UserId { get; set; }
}
