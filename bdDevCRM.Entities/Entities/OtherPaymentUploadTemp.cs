using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class OtherPaymentUploadTemp
{
    public string? EmployeeId { get; set; }

    public decimal? CtcAmount { get; set; }

    public decimal? Tax { get; set; }
}
