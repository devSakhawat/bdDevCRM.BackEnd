using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class CopyToDetails
{
    public int CopyToDetailsId { get; set; }

    public int CopyToSerial { get; set; }

    public string CopyToName { get; set; } = null!;

    public int ReferenceId { get; set; }

    public int ReferenceType { get; set; }

    public string? ReferenceTypeName { get; set; }
}
