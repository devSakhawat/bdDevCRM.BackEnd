using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class ContactDetails
{
    public int ContactId { get; set; }

    public string? Name { get; set; }

    public string? Designation { get; set; }

    public string? Email { get; set; }

    public string? PhoneNo { get; set; }

    public string? MobileNo { get; set; }

    public int? CategoryTypeId { get; set; }

    public int? IsPrimary { get; set; }

    public int? IsActive { get; set; }

    public int? ContactCompanyId { get; set; }

    public string? ContactProfilePicture { get; set; }

    public string? ContactVisitingCard { get; set; }
}
