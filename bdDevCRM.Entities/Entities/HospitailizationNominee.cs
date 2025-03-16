using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class HospitailizationNominee
{
    public int HospitalizationNomineeId { get; set; }

    public int? RelationshipId { get; set; }

    public string? NomineeName { get; set; }

    public string? Age { get; set; }

    public int? HrRecordId { get; set; }

    public int? Gender { get; set; }

    public int? CreateUser { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? UpdateUser { get; set; }

    public DateTime? UpdateDate { get; set; }

    public DateTime? BirthDate { get; set; }
}
