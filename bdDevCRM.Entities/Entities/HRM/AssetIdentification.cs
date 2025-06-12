using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class AssetIdentification
{
    public int AssetIdentificationId { get; set; }

    public string? AssetIdentificationCode { get; set; }

    public string? AssetIdentificationName { get; set; }

    public int? AssetCategoryId { get; set; }

    public string? AssetIdentificationBarCode { get; set; }
}
