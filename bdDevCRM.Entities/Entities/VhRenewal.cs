using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class VhRenewal
{
    public int RenewalId { get; set; }

    public string? RenewalType { get; set; }

    public DateTime? RenewalDate { get; set; }

    public string? InsuranceNo { get; set; }

    public int? VehicleId { get; set; }

    public bool? Status { get; set; }

    public string? ServiceMiles { get; set; }

    public DateTime? FitnessRenewalDate { get; set; }

    public DateTime? InsuranceRenewalDate { get; set; }

    public DateTime? TaxRenewalDate { get; set; }

    public int? RenewalTypeId { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? RenewalDocument { get; set; }
}
