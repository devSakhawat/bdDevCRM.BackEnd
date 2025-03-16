using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class EmployeeAssetTemp
{
    public int EmployeeAssetTempId { get; set; }

    public string? EmployeeId { get; set; }

    public string? AssetType { get; set; }

    public string? AssetName { get; set; }

    public string? AssetDescription { get; set; }

    public string? IsValid { get; set; }

    public DateOnly? IssueDate { get; set; }

    public int? UserId { get; set; }
}
