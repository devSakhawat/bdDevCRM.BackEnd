using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class AitFileAttachment
{
    public int AttachFileId { get; set; }

    public int? AitId { get; set; }

    public string? AttachedDocument { get; set; }

    public string? TitleOfDocument { get; set; }

    public string? Describtion { get; set; }
}
