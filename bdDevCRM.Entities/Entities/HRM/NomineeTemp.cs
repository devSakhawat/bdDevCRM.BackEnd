using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class NomineeTemp
{
    public int NomineTempId { get; set; }

    public string EmployeeId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Ofsahre { get; set; } = null!;

    public string? Address { get; set; }

    public DateOnly? Dateofbirth { get; set; }

    public string? Fathername { get; set; }

    public string? Mothername { get; set; }

    public string? Nationalid { get; set; }

    public string? Passport { get; set; }

    public string? Countryvisited { get; set; }

    public string? Diseases { get; set; }

    public string? Photo { get; set; }

    public string? RelationShipName { get; set; }

    public string? Occupation { get; set; }

    public string? IsActive { get; set; }

    public int? UserId { get; set; }
}
