using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class Designation
{
    public int Designationid { get; set; }

    public string Designationname { get; set; } = null!;

    public string DesignationCode { get; set; } = null!;

    public int? Status { get; set; }

    public int? DsortOrder { get; set; }

    public int? ParentDesignationId { get; set; }

    public int? DesignationTypeId { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }
}
