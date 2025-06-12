using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class IndexSectionDetails
{
    public int IndexSectionDetailsId { get; set; }

    public int? IndexSectionId { get; set; }

    public string? IndexSectionDetailsName { get; set; }

    public string? LinkUrl { get; set; }

    public int? IsActive { get; set; }
}
