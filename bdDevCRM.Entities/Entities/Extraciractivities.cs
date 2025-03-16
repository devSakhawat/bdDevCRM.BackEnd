using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class Extraciractivities
{
    public int Extraciractivitiesid { get; set; }

    public int? Cirtype { get; set; }

    public string? Description { get; set; }

    public string? Achivement { get; set; }

    public int Hrrecordid { get; set; }
}
