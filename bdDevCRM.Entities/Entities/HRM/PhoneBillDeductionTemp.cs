using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class PhoneBillDeductionTemp
{
    public string? EmployeePf { get; set; }

    public decimal? TranId { get; set; }

    public int? EmployeeId { get; set; }

    public DateTime? EffectiveMonth { get; set; }

    public DateTime? PostingDate { get; set; }

    public string? TphoneNumber { get; set; }

    public DateTime? TbillMonth { get; set; }

    public decimal? TbillAmount { get; set; }

    public decimal? TlimitAmount { get; set; }

    public decimal? Tdeduction { get; set; }

    public string? MphoneNumber { get; set; }

    public DateTime? MbillMonth { get; set; }

    public decimal? MbillAmount { get; set; }

    public decimal? MlimitAmount { get; set; }

    public decimal? Mdeduction { get; set; }

    public decimal? VoiceMail { get; set; }

    public int? InitiatedBy { get; set; }

    public int? LastModifiedBy { get; set; }

    public int? AuthorizedBy { get; set; }
}
