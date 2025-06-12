using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class ResignationApplication
{
    public int ResignationApplicationId { get; set; }

    public int? HrRecordId { get; set; }

    public DateTime? ResignationDate { get; set; }

    public string? Reason { get; set; }

    public DateTime? AppliedDate { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateUser { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? UpdateUser { get; set; }

    public int? StateId { get; set; }

    public bool? IsAcepted { get; set; }
}
