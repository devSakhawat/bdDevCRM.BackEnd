using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class ChallanNumberUpload
{
    public int ChallanNumberId { get; set; }

    public int? HrRecordId { get; set; }

    public string? ChallanNumber { get; set; }

    public decimal? Ammount { get; set; }

    public int? TaxYearId { get; set; }

    public int? UserId { get; set; }

    public DateOnly? TaxDate { get; set; }

    public string? BankBranchName { get; set; }

    public string? ReferenceNumber { get; set; }

    public string? BankName { get; set; }

    public string? CarryingPerson { get; set; }

    public string? ChequeNumber { get; set; }

    public DateTime? ChallanDate { get; set; }

    public DateTime? PayOrderDate { get; set; }

    public string? PobankId { get; set; }

    public string? PobankBranchId { get; set; }

    public string? SalaryYearId { get; set; }

    public string? AssesmentYearId { get; set; }

    public decimal? TotalAmountInChallan { get; set; }

    public DateTime? ChallanSubmissionDate { get; set; }
}
