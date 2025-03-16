using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class EmployeeContact
{
    public int EmergencyContactId { get; set; }

    public int? HrRecordId { get; set; }

    public string? ContactName { get; set; }

    public string? ContactRelation { get; set; }

    public string? ContactMobile { get; set; }

    public string? ContactAddress { get; set; }
}
