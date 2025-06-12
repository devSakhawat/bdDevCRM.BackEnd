using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class FfTaDaHistory
{
    public int TaDaIdHistory { get; set; }

    public int? HrRecordId { get; set; }

    public int? CompanyId { get; set; }

    public decimal? Tada { get; set; }

    public string? IsCurrentTerritory { get; set; }

    public int? CreateUser { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? UpdateUser { get; set; }

    public DateTime? UpdateDate { get; set; }
}
