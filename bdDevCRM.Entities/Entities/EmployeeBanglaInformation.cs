using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class EmployeeBanglaInformation
{
    public int EmployeeBanglaInformationId { get; set; }

    public int HrrecordId { get; set; }

    public string EmployeeId { get; set; } = null!;

    public string? FullName { get; set; }

    public string? BloodGroup { get; set; }

    public string? FatherName { get; set; }

    public string? MotherName { get; set; }

    public string? SpouseName { get; set; }

    public string? PresentAddress { get; set; }

    public string? PermanentAddress { get; set; }
}
