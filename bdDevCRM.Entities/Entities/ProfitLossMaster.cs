using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class ProfitLossMaster
{
    public int ProfitLossMasterId { get; set; }

    public int SubjectOfAccountId { get; set; }

    public int FinancialYearId { get; set; }

    public decimal? ProvisionForTax { get; set; }

    public DateOnly? FromDate { get; set; }

    public DateOnly? ToDate { get; set; }

    public decimal? TotalPlAmount { get; set; }

    public decimal? ProvisionForExp { get; set; }

    public decimal? UndistributedPl { get; set; }

    public decimal? DistributedPl { get; set; }

    public string? VoucharNo { get; set; }

    public string? PlVoucherNo { get; set; }
}
