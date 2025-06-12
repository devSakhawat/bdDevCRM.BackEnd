using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class PurchaseOrderDetails
{
    public int PurchaseOrderId { get; set; }

    public int? ProjectId { get; set; }

    public DateTime? Podate { get; set; }

    public string? Povalue { get; set; }

    public string? DeliveryTime { get; set; }

    public string? PoCondition { get; set; }

    public string? PoRemarks { get; set; }

    public int? PostatusId { get; set; }

    public int? IsActive { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? UpdateBy { get; set; }

    public string? PoNumber { get; set; }

    public int? ClientId { get; set; }

    public int? PoTypeId { get; set; }

    public int? ConTypeId { get; set; }

    public string? SubClientId { get; set; }
}
