using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class TempActualBasic
{
    public int Id { get; set; }

    public string EmpId { get; set; } = null!;

    public int ActualBasic { get; set; }
}
