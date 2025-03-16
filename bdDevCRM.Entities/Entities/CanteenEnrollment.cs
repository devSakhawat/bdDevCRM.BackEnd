using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class CanteenEnrollment
{
    public int EnrollmentId { get; set; }

    public int HrrecordId { get; set; }

    public string CompanyId { get; set; } = null!;

    public bool IsActive { get; set; }

    public int CanteenId { get; set; }
}
