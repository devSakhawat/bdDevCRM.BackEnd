using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class TransferPromotionTemp
{
    public int TransferPromotionTempId { get; set; }

    public string EmployeeId { get; set; } = null!;

    public string RepostingType { get; set; } = null!;

    public DateTime? EffectiveDate { get; set; }

    public string? AttendanceCardNo { get; set; }

    public string? EmployeeType { get; set; }

    public string? Company { get; set; }

    public string? Branch { get; set; }

    public string? Division { get; set; }

    public string? Facility { get; set; }

    public string? Section { get; set; }

    public string? Department { get; set; }

    public string? Designation { get; set; }

    public string? FunctionJobRole { get; set; }

    public DateTime? ConfirmationDate { get; set; }

    public string? ReportTo { get; set; }

    public string? Approver { get; set; }

    public string? TelephoneExtension { get; set; }

    public string? Grade { get; set; }

    public string? IsFieldForce { get; set; }

    public string? IsOteligible { get; set; }

    public int? UserId { get; set; }

    public DateTime? LastUpdateDate { get; set; }
}
