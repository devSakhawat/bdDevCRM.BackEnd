using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class TrusteeBoardSignatory
{
    public int TrusteeBoardId { get; set; }

    public string? ContactPerson { get; set; }

    public string? SignatoryTitle { get; set; }

    public DateOnly? Date { get; set; }

    public int? SignatoryHrRecordId { get; set; }

    public string? SignatoryName { get; set; }
}
