using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class HospitalizationAttachment
{
    public int HospitalizationAttachmentId { get; set; }

    public int? HospitalizationId { get; set; }

    public string? HospitalizationAttachmentName { get; set; }

    public string? AttachedDocument { get; set; }

    public virtual Hospitalization? Hospitalization { get; set; }
}
