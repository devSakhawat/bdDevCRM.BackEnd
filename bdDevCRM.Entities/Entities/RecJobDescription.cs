using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class RecJobDescription
{
    public int JobDescriptionId { get; set; }

    public int? JobId { get; set; }

    public string? PurposeOfJob { get; set; }

    public string? JobResponsibilitiesA { get; set; }

    public string? JobResponsibilitiesB { get; set; }

    public string? JobResponsibilitiesC { get; set; }

    public string? JobResponsibilitiesD { get; set; }

    public string? JobResponsibilitiesE { get; set; }

    public string? JobResponsibilitiesF { get; set; }

    public string? JobResponsibilitiesG { get; set; }

    public string? JobResponsibilitiesH { get; set; }

    public string? JobResponsibilitiesI { get; set; }

    public string? JobResponsibilitiesJ { get; set; }

    public string? RequiredSkillA { get; set; }

    public string? RequiredSkillB { get; set; }

    public string? RequiredSkillC { get; set; }

    public string? RequiredSkillD { get; set; }

    public string? RequiredSkillE { get; set; }

    public string? RequiredSkillF { get; set; }

    public string? RequiredQualification { get; set; }

    public string? RequiredIndustry { get; set; }

    public string? AreaOfExperience { get; set; }

    public string? RequiredCertification { get; set; }

    public decimal? AgeMinimum { get; set; }

    public decimal? AgeMaximum { get; set; }

    public string? ExperienceMinimum { get; set; }

    public string? ExperienceMaximum { get; set; }

    public int? Gender { get; set; }

    public int? AddedBy { get; set; }

    public DateTime? AddedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? JobIdSelectionMasterId { get; set; }

    public int? RequisitionRefId { get; set; }

    public decimal? SalaryMinimum { get; set; }

    public decimal? SalaryMaximum { get; set; }

    public int? IsApprovedByManagement { get; set; }

    public int? IsNotApprovedByManagement { get; set; }

    public string? ReasonOfApproved { get; set; }

    public int? IsNewRecruitment { get; set; }

    public int? IsReplacement { get; set; }

    public int? IsResignation { get; set; }

    public int? IsPromotion { get; set; }

    public int? IsTransfer { get; set; }

    public int? IsDisciplinaryAction { get; set; }

    public int? IsDeath { get; set; }

    public int? ReplacementEmployeeHrrecordId { get; set; }

    public int? EmploymentTypeId { get; set; }

    public int? EmployeeCategoryId { get; set; }

    public int? IsSalaryNegotiable { get; set; }

    public string? LanguageProficiency { get; set; }

    public string? ComputerLiteracy { get; set; }

    public string? Remarks { get; set; }
}
