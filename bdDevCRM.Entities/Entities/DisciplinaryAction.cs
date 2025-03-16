using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class DisciplinaryAction
{
    public int DisciplinaryActionId { get; set; }

    public int HrrecordId { get; set; }

    public int NatureOfActionId { get; set; }

    public DateTime DateofPunishment { get; set; }

    public DateTime? ReleasefromPunishment { get; set; }

    public string? PunishmentDetails { get; set; }

    public string? Uploadfilepath { get; set; }
}
