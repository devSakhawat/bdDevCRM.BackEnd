using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class DeviceRawTimeRecord2018
{
    public int DeviceRawTimeRecordId { get; set; }

    public string? DevId { get; set; }

    public string? CardId { get; set; }

    public DateTime? CardTime { get; set; }

    public string? EmployeeName { get; set; }

    public string? WorkCode { get; set; }

    public int? Status { get; set; }

    public string? Authority { get; set; }

    public string? CardSrc { get; set; }

    public int? IsProcess { get; set; }
}
