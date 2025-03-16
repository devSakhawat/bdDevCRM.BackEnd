using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class Nominee
{
    public int NomineeId { get; set; }

    public int HrrecordId { get; set; }

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

    public int? RelationshipId { get; set; }

    public int? OccupationId { get; set; }

    /// <summary>
    /// 1=Active,0=Inactive
    /// </summary>
    public int? IsActive { get; set; }

    public string? TmpId { get; set; }

    public string? ContactNumber { get; set; }

    public int? Age { get; set; }

    public virtual Occupation? Occupation { get; set; }
}
