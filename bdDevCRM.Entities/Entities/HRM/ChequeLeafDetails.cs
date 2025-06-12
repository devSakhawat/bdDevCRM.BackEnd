using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class ChequeLeafDetails
{
    public int ChequeLeafDetailsId { get; set; }

    public int? ChequeLeafMasterId { get; set; }

    public int? LeafNo { get; set; }

    public int? IsUsed { get; set; }

    public int? IsPrinted { get; set; }
}
