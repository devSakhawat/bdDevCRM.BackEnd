using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class Education
{
    public int EducationHistoryId { get; set; }

    public int HrrecordId { get; set; }

    public string? Certificate { get; set; }

    public string? Institute { get; set; }

    public string? Yearofcompletion { get; set; }

    public string? Result { get; set; }

    public string? Board { get; set; }

    public int? Certificatetypeid { get; set; }

    public int? ResultStatusId { get; set; }

    public int? IsConsiderInc { get; set; }
}
