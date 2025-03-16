using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class CnfAgents
{
    public int AgentId { get; set; }

    public string AgentName { get; set; } = null!;

    public string AgentCode { get; set; } = null!;

    public int AgencyId { get; set; }

    public string? FatherName { get; set; }

    public string? PerAddress { get; set; }

    public string? LocalAddress { get; set; }

    public string? JslN0 { get; set; }

    public string? PoliceVerification { get; set; }

    public string? Photo { get; set; }

    public int? Isactive { get; set; }
}
