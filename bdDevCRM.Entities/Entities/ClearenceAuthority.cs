using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class ClearenceAuthority
{
    public int ClearenceAuthorityId { get; set; }

    public int HrRecordId { get; set; }

    public int CompanyId { get; set; }

    public int DepartmentId { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsSendEmail { get; set; }

    public bool? IsApplyToAll { get; set; }
}
