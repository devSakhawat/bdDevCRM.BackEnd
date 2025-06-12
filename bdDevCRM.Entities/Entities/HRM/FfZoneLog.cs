using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class FfZoneLog
{
    public int ZoneLogId { get; set; }

    public string? ZoneName { get; set; }

    public string? ZoneCode { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? CreateUser { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? UpdateUser { get; set; }

    public int? Status { get; set; }

    public int? ZoneId { get; set; }

    public int? CompanyId { get; set; }
}
