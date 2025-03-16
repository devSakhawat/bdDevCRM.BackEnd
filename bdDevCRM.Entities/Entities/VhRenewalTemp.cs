using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class VhRenewalTemp
{
    public int RenewalId { get; set; }

    public DateTime? TaxRenewalDate { get; set; }

    public DateTime? FitnessRenewalDate { get; set; }

    public string? InsuranceNo { get; set; }

    public DateTime? InsuranceRenewalDate { get; set; }

    public string? ServiceMiles { get; set; }

    public int? Status { get; set; }

    public int? VehicleId { get; set; }
}
