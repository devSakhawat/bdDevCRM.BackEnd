using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class ConfirmationPolicy
{
    public int ConfirmationPolicyId { get; set; }

    public string? ConfirmationPolicyName { get; set; }

    public int? ConfirmationPolicyDuration { get; set; }

    public int? EmployeeTypeId { get; set; }

    public int? IsActive { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }
}
