using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class LetterInfoJobConfirmation
{
    public int LetterInfoId { get; set; }

    public int? HrRecordId { get; set; }

    public string? SignatoryId { get; set; }

    public string? SignatoryName { get; set; }

    public string? SignatoryDesignation { get; set; }

    public string? CopyTo { get; set; }

    public string? LetterSignature { get; set; }

    public string? FreeTextt { get; set; }

    public string? ReferenceNo { get; set; }

    public int? JobConfirmationMasterId { get; set; }

    public DateTime? LetterIssueDate { get; set; }
}
