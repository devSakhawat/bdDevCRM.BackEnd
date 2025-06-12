using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class MobileCeilingTemp
{
    public int MobCeilingId { get; set; }

    public string? EmployeeId { get; set; }

    public string? ContactNo { get; set; }

    public string? TotalCeiling { get; set; }

    public string? IsdCeiling { get; set; }

    public string? VasCeiling { get; set; }

    public string? SmsCeiling { get; set; }

    public string? InternetCeiling { get; set; }

    public int? UserId { get; set; }

    public int? DeviceType { get; set; }

    public DateTime? SimIssueDate { get; set; }
}
