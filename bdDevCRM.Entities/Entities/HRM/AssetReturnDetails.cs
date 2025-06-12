using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class AssetReturnDetails
{
    public int AssetReturnDetailsId { get; set; }

    public int? Hrrecordid { get; set; }

    public DateTime? ReturnDate { get; set; }

    public string? ReceivingGoodsConditionId { get; set; }

    public string? Disposual { get; set; }

    public string? Remarks { get; set; }

    public int? ReceiverId { get; set; }

    public string? Place { get; set; }

    public int? Assetid { get; set; }
}
