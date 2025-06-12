using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class HrDocument
{
    public int HrDocumentId { get; set; }

    public string? ReportFileName { get; set; }

    public string? DocumentName { get; set; }

    public int? AssemblyId { get; set; }

    public string? FolderPath { get; set; }

    public bool? IsActive { get; set; }

    public int? ReportHeaderId { get; set; }

    public int? DocumentTypeId { get; set; }

    public int? DataSourceId { get; set; }
}
