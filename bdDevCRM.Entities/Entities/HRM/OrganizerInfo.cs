using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class OrganizerInfo
{
    public int OrganizerId { get; set; }

    public string? OrganizerCode { get; set; }

    public string OrganizerName { get; set; } = null!;

    public int? OrganizerType { get; set; }

    public int? IsOrganizerActive { get; set; }
}
