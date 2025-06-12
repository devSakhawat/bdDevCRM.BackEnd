using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class Asset
{
    public int Assetid { get; set; }

    public string Assetdescription { get; set; } = null!;

    public DateTime Issuedate { get; set; }

    public DateTime? Returndate { get; set; }

    public string? Assetstatus { get; set; }

    public int Hrrecordid { get; set; }

    public string? Assetname { get; set; }

    public int? Isreturn { get; set; }

    public int? Assetidentificationid { get; set; }
}
