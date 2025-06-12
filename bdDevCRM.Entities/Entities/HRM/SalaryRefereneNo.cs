using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class SalaryRefereneNo
{
    public DateOnly? SalaryMonth { get; set; }

    public string? ReferenceNo { get; set; }

    public int? UniqueNumber { get; set; }
}
