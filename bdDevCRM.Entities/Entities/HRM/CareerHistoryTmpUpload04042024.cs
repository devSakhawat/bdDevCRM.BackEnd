using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class CareerHistoryTmpUpload04042024
{
    public string? EmployeeId { get; set; }

    public string? FullName { get; set; }

    public string? FromPayScale { get; set; }

    public string? ToPayScale { get; set; }

    public string? PostingTypeName { get; set; }

    public DateOnly? EffectiveStartDate { get; set; }

    public DateOnly? EffectiveEndDate { get; set; }

    public string? FromDesignation { get; set; }

    public string? ToDesignation { get; set; }

    public string? FromCompany { get; set; }

    public string? ToCompany { get; set; }

    public string? FromLocation { get; set; }

    public string? ToLocation { get; set; }

    public string? FromCostCentre { get; set; }

    public string? ToCostCentre { get; set; }

    public string? FormDepertment { get; set; }

    public string? ToDepertment { get; set; }

    public string? Remark { get; set; }

    public string? Note { get; set; }

    public string? PostingTypeRemarks { get; set; }
}
