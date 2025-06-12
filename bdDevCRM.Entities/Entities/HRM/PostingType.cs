using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class PostingType
{
    public int PostingTypeId { get; set; }

    public string PostingTypeName { get; set; } = null!;

    public int IsActive { get; set; }

    public int? IsConsiderForPms { get; set; }

    public virtual ICollection<TransferPromotion> TransferPromotion { get; set; } = new List<TransferPromotion>();
}
