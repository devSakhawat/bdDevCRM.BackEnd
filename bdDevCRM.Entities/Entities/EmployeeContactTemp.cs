using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class EmployeeContactTemp
{
    public int NewEmergencyContactId { get; set; }

    public string? EmployeeId { get; set; }

    public string? ContactName { get; set; }

    public string? ContactRelation { get; set; }

    public string? ContactMobile { get; set; }

    public string? ContactAddress { get; set; }

    public int? UserId { get; set; }
}
