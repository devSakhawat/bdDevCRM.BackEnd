using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class CnfAgency
{
    public int Id { get; set; }

    public string AgencyName { get; set; } = null!;

    public int? AgencyTypeId { get; set; }

    public string? AgencyCode { get; set; }

    public string? ContactPerson { get; set; }

    public string? ContactNumber { get; set; }

    public string? OfficePhoneNo { get; set; }

    public string? AgencyAddress { get; set; }
}
