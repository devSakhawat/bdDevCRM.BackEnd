using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class News
{
    public int NewsId { get; set; }

    public int? NewsCategoryId { get; set; }

    public string? NewsTitle { get; set; }

    public string? NewsDetails { get; set; }

    public DateTime? PublishDate { get; set; }

    public DateTime? ExpiryDate { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }
}
