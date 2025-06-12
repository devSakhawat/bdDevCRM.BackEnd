using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class OldSalary
{
    public int SalaryId { get; set; }

    public int HrRecordId { get; set; }

    public DateTime SalaryMonth { get; set; }

    public DateTime GenerateDate { get; set; }

    public decimal GrossPay { get; set; }

    public decimal? Arear { get; set; }

    public decimal? CoffEncash { get; set; }

    public decimal? LeaveEncash { get; set; }

    public decimal? OtherAddition { get; set; }

    public decimal? Telephone { get; set; }

    public decimal? Css { get; set; }

    public decimal? Insurence { get; set; }

    public decimal? Advance { get; set; }

    public decimal? LateLeave { get; set; }

    public int? LeaveDeducted { get; set; }

    public decimal? OtherDeduction { get; set; }

    public decimal? Ait { get; set; }

    public decimal? NetPay { get; set; }

    public int? StateId { get; set; }

    public string? PaymentMode { get; set; }

    public bool? IsClear { get; set; }

    public int? MessageId { get; set; }

    public int? UserId { get; set; }

    public DateTime? LastUpdateDate { get; set; }

    public int? ApproverId { get; set; }

    public DateTime? ApproveDate { get; set; }

    public int? Withoutpay { get; set; }

    public int? Cssemployer { get; set; }

    public int? Overtime { get; set; }

    public int? Payrollid { get; set; }
}
