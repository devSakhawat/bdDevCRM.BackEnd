using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class OtAmountSetUpInfo
{
    public int OtAmountSetUpId { get; set; }

    public int HrRecordId { get; set; }

    public int? OtAmountTypeId { get; set; }

    public string? OtAmountTypeName { get; set; }

    public decimal? OtAmount { get; set; }

    public string? Remarks { get; set; }

    public int? IsActive { get; set; }

    public int? InsertBy { get; set; }

    public DateTime? InsertDate { get; set; }

    public int? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }
}
