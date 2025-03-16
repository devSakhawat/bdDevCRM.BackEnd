using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class AttachFileForPo
{
    public int AttachFileId { get; set; }

    public int? PurchaseOrderId { get; set; }

    public string? AttachedDocument { get; set; }

    public string? TitleOfDocument { get; set; }

    public string? Describtion { get; set; }
}
