using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities.DMS;

public partial class DmsdocumentType
{
    public int DocumentTypeId { get; set; }

    public string Name { get; set; } = null!;

    public string DocumentType { get; set; } = null!;

    public bool IsMandatory { get; set; }

    public string? AcceptedExtensions { get; set; }

    public int? MaxFileSizeMb { get; set; }

    public virtual ICollection<Dmsdocument> Dmsdocument { get; set; } = new List<Dmsdocument>();
}
