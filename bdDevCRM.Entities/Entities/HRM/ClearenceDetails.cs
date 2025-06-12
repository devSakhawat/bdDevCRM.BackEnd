using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class ClearenceDetails
{
    public int ClearenceDetailsId { get; set; }

    public int ClearenceAuthorityId { get; set; }

    public int ApplicationId { get; set; }

    public string? Remarks { get; set; }

    public int IsCleared { get; set; }

    public DateOnly ClearedDate { get; set; }
}
