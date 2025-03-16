using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class Client
{
    public int ClientId { get; set; }

    public string? ClientCode { get; set; }

    public string? ClientName { get; set; }

    public string? Address { get; set; }

    public string? Email { get; set; }

    public string? PrimaryContact { get; set; }

    public string? ContactNo { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsMovement { get; set; }
}
