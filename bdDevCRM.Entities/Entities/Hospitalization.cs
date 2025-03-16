using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class Hospitalization
{
    public int HospitalizationId { get; set; }

    public int HrRecordId { get; set; }

    public int PatientId { get; set; }

    public int? HospitalId { get; set; }

    public DateOnly? AdmissionDate { get; set; }

    public string? TreatmentDetails { get; set; }

    public DateOnly? ReleaseDate { get; set; }

    public decimal? ClaimedAmount { get; set; }

    public int? RecommenderId { get; set; }

    public DateOnly? RecommendDate { get; set; }

    public int? ApproverId { get; set; }

    public DateOnly? ApprovedDate { get; set; }

    public int? StatusId { get; set; }

    public string? Relationship { get; set; }

    public int? CompanyId { get; set; }

    public int? BranchId { get; set; }

    public int? DepartmentId { get; set; }

    public int? DesignationId { get; set; }

    public int? GradeId { get; set; }

    public int? EmployeeTypeId { get; set; }

    public decimal? TotalAmount { get; set; }

    public decimal? RemaningBalance { get; set; }

    public decimal? AvailledAmount { get; set; }

    public virtual ICollection<HospitalizationAttachment> HospitalizationAttachment { get; set; } = new List<HospitalizationAttachment>();
}
