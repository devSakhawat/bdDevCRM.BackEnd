using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class FfDailyFieldBenifitMapping
{
    public int FfMappingId { get; set; }

    public int? FieldBenifitCategoryId { get; set; }

    public int? ReferenceId { get; set; }

    public int? ReferenceType { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? IsActive { get; set; }

    public int? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }
}
