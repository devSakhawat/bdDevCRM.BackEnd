using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class Diseasesinformation
{
    public int Diseasesinformationid { get; set; }

    public string? Description { get; set; }

    public DateTime? Disdate { get; set; }

    public string? Disstatus { get; set; }

    public int Hrrecordid { get; set; }
}
