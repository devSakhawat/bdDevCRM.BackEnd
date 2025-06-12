using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class Discipline
{
    public int DisciplineId { get; set; }

    public string? DisciplineName { get; set; }

    public int? CertificateTypeId { get; set; }

    public int? IsActive { get; set; }
}
