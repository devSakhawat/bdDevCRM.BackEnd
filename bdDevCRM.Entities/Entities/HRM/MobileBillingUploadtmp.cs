using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class MobileBillingUploadtmp
{
    public int MobileBillingTempId { get; set; }

    public string? MobileNo { get; set; }

    public string? IsdAmount { get; set; }

    public string? VasAmount { get; set; }

    public string? SmsAmount { get; set; }

    public string? InternetAmount { get; set; }

    public string? BillingMonth { get; set; }

    public int? UserId { get; set; }

    public string? SalaryDate { get; set; }

    public int? SimVendorId { get; set; }

    public decimal? TotalAmount { get; set; }
}
