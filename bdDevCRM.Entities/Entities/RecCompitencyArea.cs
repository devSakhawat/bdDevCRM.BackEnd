using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class RecCompitencyArea
{
    public int Id { get; set; }

    public int CompitencyId { get; set; }

    public string? CompitencyAreaName { get; set; }

    public string? Description { get; set; }

    public int? OutOfMarks { get; set; }

    public decimal? MaxMarks { get; set; }

    public int? ImportanceRate { get; set; }

    public decimal? MaxMark { get; set; }

    public int? SortOrder { get; set; }

    public int? IsActive { get; set; }

    public decimal? MinMarks { get; set; }

    public int? IsRecruitment { get; set; }
}
