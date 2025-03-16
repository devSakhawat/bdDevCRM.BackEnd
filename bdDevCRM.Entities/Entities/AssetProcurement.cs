using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class AssetProcurement
{
    public int AssetProcurementId { get; set; }

    public int AssetIdentificationId { get; set; }

    public DateOnly? ProcurementDate { get; set; }

    public string? VendorName { get; set; }

    public string? SupplierAddress { get; set; }

    public string? AssetWarrantyPeriod { get; set; }

    public decimal? ProcurementValue { get; set; }

    public string? ProductSlNo { get; set; }

    public string? ProductName { get; set; }

    public string? ProductModel { get; set; }

    public string? CountryOrigin { get; set; }

    public string? Remarks { get; set; }

    public string? ManufacturingYear { get; set; }

    public DateOnly? AssignmentDate { get; set; }

    public DateOnly? AssetWarrantyEndDate { get; set; }
}
