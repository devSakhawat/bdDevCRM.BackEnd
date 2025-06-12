using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class ContactCompany
{
    public int ContactCompanyId { get; set; }

    public string? ContactCompanyName { get; set; }

    public string? CustomerCode { get; set; }

    public string? Address { get; set; }

    public string? Website { get; set; }

    public string? PhoneNo { get; set; }

    public string? Email { get; set; }

    public string? MobileNo { get; set; }

    public int? PrimaryType { get; set; }

    public int? CategoryTypeId { get; set; }

    public int? IsActive { get; set; }
}
