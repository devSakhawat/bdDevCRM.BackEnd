using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class RevDistributionMaster
{
    public int RevDistributionMasterId { get; set; }

    public int CompanyId { get; set; }

    public int FundYearId { get; set; }

    public decimal TotalContribution { get; set; }

    public decimal TotalRevenue { get; set; }

    public decimal? EmployeeRevenue { get; set; }

    public decimal? EmployerRevenue { get; set; }

    public decimal TotalProduct { get; set; }

    public decimal EmployeeActive { get; set; }

    public decimal? EmployerActive { get; set; }

    public decimal? EmployeeInActive { get; set; }

    public decimal EmployerInActive { get; set; }

    public decimal RevRatioActiveOwn { get; set; }

    public DateOnly? Month { get; set; }

    public int? MakeBy { get; set; }

    public DateOnly? MakeDate { get; set; }
}
