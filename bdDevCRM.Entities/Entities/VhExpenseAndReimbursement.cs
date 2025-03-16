using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class VhExpenseAndReimbursement
{
    public int ExpnsAndReimbrsmntId { get; set; }

    public int? VehicleId { get; set; }

    public int? Status { get; set; }

    public DateOnly? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? ServicingDate { get; set; }

    public string? FilePath { get; set; }

    public int? RouteId { get; set; }

    public int? ManagerHrRecordId { get; set; }

    public int? VehicleCompanyId { get; set; }

    public int? VehicleBranchId { get; set; }
}
