using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class UnauthorizedAbsenceDetails
{
    public int UnauthorizedAbsenceDetailsId { get; set; }

    public int UnauthorizedAbsenceId { get; set; }

    public int ReportTypeId { get; set; }

    public string LetterRefNo { get; set; } = null!;

    public DateOnly LetterIssueDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public int? AbsentDays { get; set; }

    public int SignatoriesHrRecordId { get; set; }

    public string SignatoriesDesignation { get; set; } = null!;

    public string? SignatoriesCompany { get; set; }

    public int CreateBy { get; set; }

    public DateTime CreateDate { get; set; }

    public int? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }
}
