using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class AttendenceCode
{
    public int AttendenceCodeId { get; set; }

    public string? AttendenceCode1 { get; set; }

    public string? AttendenceName { get; set; }

    public int? IsActive { get; set; }
}
