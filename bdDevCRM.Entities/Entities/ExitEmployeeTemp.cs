using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class ExitEmployeeTemp
{
    public int ExitEmployeeTempId { get; set; }

    public string EmployeeId { get; set; } = null!;

    public DateTime? JobEndDate { get; set; }

    public string? JobEndType { get; set; }

    public string? Status { get; set; }

    public int? UserId { get; set; }
}
