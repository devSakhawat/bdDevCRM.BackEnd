using System;

namespace bdDevCRM.Shared.DataTransferObjects.CRM;

public class AdditionalInfoDto
{
  public int AdditionalInfoId { get; set; }
  public int ApplicantId { get; set; } // Foreign Key
  public string? RequireAccommodation { get; set; }
  public string? HealthNMedicalNeeds { get; set; }
  public string? HealthNMedicalNeedsRemarks { get; set; }
  public string? AdditionalInformationRemarks { get; set; }
  
  // Common fields
  public DateTime CreatedDate { get; set; }
  public int CreatedBy { get; set; }
  public DateTime? UpdatedDate { get; set; }
  public int? UpdatedBy { get; set; }
}