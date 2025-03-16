using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class AppraisalRattingSlab
{
    public int RattingSlabId { get; set; }

    public decimal? FromRattingValue { get; set; }

    public decimal? ToRattingValue { get; set; }

    public string? RattingStatus { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateUser { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? UpdateUser { get; set; }
}
