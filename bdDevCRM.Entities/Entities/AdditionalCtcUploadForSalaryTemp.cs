using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class AdditionalCtcUploadForSalaryTemp
{
    public int UserId { get; set; }

    public string? EmployeeId { get; set; }

    public decimal? CtcAmount { get; set; }
}
