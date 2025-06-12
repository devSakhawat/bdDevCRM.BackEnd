using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class ShiftMapping
{
    public int ShiftMapId { get; set; }

    public int? ShiftId { get; set; }

    public int? CompanyId { get; set; }

    public int? ReferenceId { get; set; }

    public decimal? LunchHour { get; set; }

    public int? ReferenceType { get; set; }

    public int? LocationId { get; set; }

    public int? DepartmentId { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }
}
