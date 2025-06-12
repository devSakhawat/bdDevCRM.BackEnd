using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class LetterGeneration
{
    public int LetterGenerationId { get; set; }

    public int? PerformanceReviewid { get; set; }

    public string? SearchById { get; set; }

    public string? ChroName { get; set; }

    public string? ChroDesignation { get; set; }

    public string? DesignationExtension { get; set; }

    public string? CopyTo { get; set; }

    public string? ReferenceNo { get; set; }

    public DateOnly? IssueDate { get; set; }

    public string? AccountHeadTo { get; set; }

    public string? EvaluationType { get; set; }

    public string? CopyToAccounts { get; set; }

    public string? LevelOf { get; set; }

    public string? Responsibility { get; set; }
}
