using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class SuspensionDetails
{
    public int SuspensionId { get; set; }

    public int? HrRecordId { get; set; }

    public DateOnly? SuspensionDate { get; set; }

    public string? Remarks { get; set; }

    public int? StateId { get; set; }

    public string? NatureOfAction { get; set; }

    public string? Review { get; set; }

    public DateOnly? ReviewDate { get; set; }

    public DateOnly? ReleaseDate { get; set; }

    public string? AttachSuspensionFile { get; set; }

    public int? GenerateBy { get; set; }

    public DateOnly? GenerateDate { get; set; }
}
