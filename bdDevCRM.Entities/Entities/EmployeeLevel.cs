using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class EmployeeLevel
{
    public int EmployeeLevelId { get; set; }

    public string? EmployeeLevelName { get; set; }

    public int? IsActive { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? Updateby { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? IsConsiderForNotify { get; set; }
}
