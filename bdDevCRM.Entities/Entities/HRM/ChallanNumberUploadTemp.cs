using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class ChallanNumberUploadTemp
{
    public int ChallanNumberId { get; set; }

    public string? EmployeeId { get; set; }

    public string? ChallanNumber { get; set; }

    public decimal? Ammount { get; set; }

    public int? TaxYearId { get; set; }

    public int? UserId { get; set; }

    public DateOnly? TaxDate { get; set; }

    public string? BankBranchName { get; set; }

    public decimal? TotalAmountInChallan { get; set; }
}
