using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class EmployeeProfileTabs
{
    public int TabId { get; set; }

    public string? TabKey { get; set; }

    public string? TabName { get; set; }

    public int? ParentTab { get; set; }

    public string? ControlType { get; set; }
}
