using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class PunishmentInfo
{
    public int PunishmentId { get; set; }

    public int? HrrecordId { get; set; }

    public DateTime? PunishmentDate { get; set; }

    public int? PunishmentTypeId { get; set; }

    public decimal? PunishmentDays { get; set; }

    public decimal? PunishmentAmount { get; set; }

    public string? Remarks { get; set; }

    public int? StateId { get; set; }

    public int? ApprovedBy { get; set; }

    public DateTime? ApprovedDate { get; set; }

    public decimal? PunishmentPercentage { get; set; }
}
