using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class VhDriver
{
    public int DriverId { get; set; }

    public string? DriverName { get; set; }

    public string? DriverPhoto { get; set; }

    public string? BloodGroup { get; set; }

    public int? IsActive { get; set; }

    public string? Remarks { get; set; }

    public string? ContactNumber { get; set; }

    public string? FatherName { get; set; }

    public string? MotherName { get; set; }

    public string? PresentAddress { get; set; }

    public string? NationalId { get; set; }

    public string? PassportNo { get; set; }

    public string? DateofBirth { get; set; }

    public int? MeritialStatus { get; set; }

    public string? LicenseNo { get; set; }

    public string? LicenseExpireDate { get; set; }

    public int? AddedBy { get; set; }

    public string? AddedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public string? UpdatedDate { get; set; }

    public int? ApprovedBy { get; set; }

    public bool? IsApproved { get; set; }

    public string? ApprovedDate { get; set; }

    public string? EmployeeId { get; set; }

    public string? EmergencyContactNumber { get; set; }

    public string? PermanentAddress { get; set; }

    public int? Religion { get; set; }

    public int? DriverType { get; set; }

    public int? LicenseType { get; set; }

    public int? CompanyId { get; set; }

    public int? BranchId { get; set; }
}
