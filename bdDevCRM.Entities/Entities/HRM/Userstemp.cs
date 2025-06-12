using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class Userstemp
{
    public int? Companyid { get; set; }

    public string Loginid { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int? Employeeid { get; set; }

    public DateTime Createddate { get; set; }

    public DateTime? Lastupdatedate { get; set; }

    public DateTime? Lastlogindate { get; set; }

    public int Failedloginno { get; set; }

    public int Isactive { get; set; }

    public int Isexpired { get; set; }

    public string? Theme { get; set; }
}
