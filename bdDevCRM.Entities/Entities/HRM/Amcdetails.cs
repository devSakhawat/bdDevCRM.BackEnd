using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class Amcdetails
{
    public int AmcInfoId { get; set; }

    public string? AmcRef { get; set; }

    public int? ProjectId { get; set; }

    public decimal? ProjectValue { get; set; }

    public int? ClientId { get; set; }

    public DateTime? SignDate { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public int? InvoiceTypeId { get; set; }

    public decimal? AmcParcentage { get; set; }

    public decimal? YearlyAmount { get; set; }

    public decimal? QuaterlyAmount { get; set; }

    public decimal? MonthlyAmount { get; set; }

    public int? AmcStatusId { get; set; }

    public int? IsActive { get; set; }
}
