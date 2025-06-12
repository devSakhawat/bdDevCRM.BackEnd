using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class AssetCategory
{
    public int AssetCategoryId { get; set; }

    public string? AssetCategoryCode { get; set; }

    public string? AssetCategoryName { get; set; }

    public int? ParentCategoryId { get; set; }

    public int? IsActive { get; set; }
}
