using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class Certificatetype
{
    public int Certificatetypeid { get; set; }

    public string? Certificatetypename { get; set; }

    public string? Certificatecode { get; set; }

    public int IsActive { get; set; }

    public int? DegreeTypeId { get; set; }
}
