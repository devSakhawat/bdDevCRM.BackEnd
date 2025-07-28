using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities.CRM;

// address should be save before applicant and two table will be one table.
public partial class PermanentAddress
{
    public int PermanentAddressId { get; set; }

    public int ApplicantId { get; set; }

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public int CountryId { get; set; }

    public string? CountryName { get; set; }

    public string? PostalCode { get; set; }

    public DateTime CreatedDate { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public virtual CrmApplication Applicant { get; set; } = null!;
}
