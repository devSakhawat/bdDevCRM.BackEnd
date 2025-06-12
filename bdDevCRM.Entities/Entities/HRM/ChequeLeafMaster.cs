using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class ChequeLeafMaster
{
    public int ChequeLeafMasterId { get; set; }

    public int? ChequeId { get; set; }

    public string? ChequebookNo { get; set; }

    public int? LeafNoFrom { get; set; }

    public int? LeafNoTo { get; set; }

    public DateTime? Date { get; set; }

    public int? IsActive { get; set; }

    public int? UserId { get; set; }
}
