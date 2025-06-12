using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class DeviceSetup1804ForDelete
{
    public int DeviceSetupId { get; set; }

    public int CompanyId { get; set; }

    public int? LocationId { get; set; }

    public string DeviceId { get; set; } = null!;

    public string DeviceType { get; set; } = null!;

    public string? DeviceModel { get; set; }

    public string? DeviceDescription { get; set; }

    public string? DevicePort { get; set; }

    public string? ConnectionType { get; set; }

    public int? IsThereSingleDevice { get; set; }

    public string? Ipaddress { get; set; }

    public string? Comport { get; set; }

    public string? DeviceUser { get; set; }

    public string? DevicePassword { get; set; }

    public int? Status { get; set; }

    public string Manufacturer { get; set; } = null!;

    public string? ServiceType { get; set; }

    public string? CatalogName { get; set; }

    public int? IsExternalDevice { get; set; }

    public int? SortingOrder { get; set; }
}
