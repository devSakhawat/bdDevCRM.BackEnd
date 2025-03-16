using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class TempApplicantInformation
{
    public int TempApplicantId { get; set; }

    public string ApplicantName { get; set; } = null!;

    public string? ApplicantPhotoPath { get; set; }

    public string? NationalId { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string? Gender { get; set; }

    public string? FatherName { get; set; }

    public string? MotherName { get; set; }

    public string? MobileNumber { get; set; }

    public string? PersonalEmail { get; set; }

    public int? AppliedPost { get; set; }

    public string? AttachmentCv { get; set; }

    public string? ApplicantAddress { get; set; }

    public string? ApplicantSurname { get; set; }

    public string? ApplicantSource { get; set; }

    public string? Qualification { get; set; }

    public string? CurrentDesignation { get; set; }

    public string? ExperienceYear { get; set; }

    public decimal? PresentSalary { get; set; }

    public string? EducationalQualification { get; set; }

    public string? Institute { get; set; }

    public string? CurrentCompany { get; set; }

    public decimal? ExpectedSalary { get; set; }

    public string? ReferenceType { get; set; }

    public string? Reference1 { get; set; }

    public string? Reference2 { get; set; }

    public string? Result { get; set; }

    public int? UserId { get; set; }

    public string? ProfessionalQualification { get; set; }

    public string? PresentCompanyExperienceYear { get; set; }

    public string? TotalExperienceYear { get; set; }

    public string? ApplicantPresentAddress { get; set; }

    public string? ApplicantPermanentAddress { get; set; }
}
