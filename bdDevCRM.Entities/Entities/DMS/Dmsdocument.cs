using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities.DMS;

public partial class Dmsdocument
{
    public int DocumentId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string FileName { get; set; } = null!;

    public string FileExtension { get; set; } = null!;

    public long FileSize { get; set; }

    public string FilePath { get; set; } = null!;

    public DateTime? UploadDate { get; set; }

    public string? UploadedByUserId { get; set; }

    public int DocumentTypeId { get; set; }

    public string? ReferenceEntityType { get; set; }

    public string? ReferenceEntityId { get; set; }

    public int? FolderId { get; set; }

    public virtual ICollection<DmsdocumentAccessLog> DmsdocumentAccessLog { get; set; } = new List<DmsdocumentAccessLog>();

    public virtual ICollection<DmsdocumentTagMap> DmsdocumentTagMap { get; set; } = new List<DmsdocumentTagMap>();

    public virtual ICollection<DmsdocumentVersion> DmsdocumentVersion { get; set; } = new List<DmsdocumentVersion>();

    public virtual DmsdocumentType DocumentType { get; set; } = null!;
}
