using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class SalaryClose
{
    public int SalaryCloseId { get; set; }

    public DateTime? SalaryMonth { get; set; }

    public int? IsSalaryClose { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? UpdatedBy { get; set; }
}
