using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class NewsCategory
{
    public int NewsCategoryId { get; set; }

    public string? NewsCategoryCode { get; set; }

    public string? NewsCategoryDescription { get; set; }

    public int? IsActive { get; set; }
}
