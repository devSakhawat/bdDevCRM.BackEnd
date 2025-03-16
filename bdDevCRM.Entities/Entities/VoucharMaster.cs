using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class VoucharMaster
{
    public int VoucharId { get; set; }

    public int? SubjectId { get; set; }

    public int? VoucharType { get; set; }

    public int? TransectionType { get; set; }

    public string? VoucharNo { get; set; }

    public DateTime? VoucharDate { get; set; }

    public string? Detaills { get; set; }

    public string? Naration { get; set; }

    public int? IsFinalPosted { get; set; }

    public decimal? TotalDebitAmount { get; set; }

    public decimal? TotalCreditAmount { get; set; }

    public int? MakeBy { get; set; }

    public DateTime? MakeDate { get; set; }

    public int? PostedBy { get; set; }

    public DateTime? PostedDate { get; set; }

    public DateOnly? ChequeDate { get; set; }

    public string? ChequeNo { get; set; }

    public string? Source { get; set; }

    public int? BankId { get; set; }

    public int? VoucherSource { get; set; }
}
