using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class EmployeeAsset
{
    public int EmployeeAssetId { get; set; }

    public int? HrRecordId { get; set; }

    public int? AssetCategoryId { get; set; }

    public string? AssetName { get; set; }

    public DateOnly? DateOfPurchaseWarranty { get; set; }

    public DateOnly? PeriodEndDate { get; set; }

    public decimal? Price { get; set; }

    public DateOnly? IssueDate { get; set; }

    public DateOnly? ReturnDate { get; set; }

    public string? Remarks { get; set; }

    public string? BrandNo { get; set; }

    public string? ModelNo { get; set; }

    public string? SerialNo { get; set; }
}
