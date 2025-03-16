using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class CanteenSupplier
{
    public int SupplierId { get; set; }

    public string SupplierCode { get; set; } = null!;

    public string SupplierName { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string PrimaryContactPerson { get; set; } = null!;

    public string BillingContactPerson { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Mobile { get; set; }

    public string? Email { get; set; }

    public bool IsActive { get; set; }
}
