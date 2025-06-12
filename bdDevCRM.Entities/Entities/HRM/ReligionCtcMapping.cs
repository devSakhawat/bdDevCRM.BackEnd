using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class ReligionCtcMapping
{
    public int MappingId { get; set; }

    public int? CtcId { get; set; }

    public int? ReligionId { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? InactiveBy { get; set; }

    public DateTime? InactiveDate { get; set; }

    public int? Status { get; set; }
}
