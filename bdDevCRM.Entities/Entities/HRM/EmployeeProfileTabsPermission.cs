using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class EmployeeProfileTabsPermission
{
    public int TabPermissionId { get; set; }

    public int? GroupId { get; set; }

    public int TabId { get; set; }

    public bool? IsVisible { get; set; }

    public bool? IsRead { get; set; }

    public bool? IsWrite { get; set; }
}
