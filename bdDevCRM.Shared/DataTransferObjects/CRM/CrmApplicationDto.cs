using System;

namespace bdDevCRM.Shared.DataTransferObjects.CRM;

/// <summary>
/// Complete CRM Application Data Transfer Object
/// </summary>
public class CrmApplicationDto
{
  public int ApplicationId { get; set; }
  public DateTime ApplicationDate { get; set; }
  public string ApplicationStatus { get; set; } = "Draft";
  
  // Common fields
  public DateTime CreatedDate { get; set; }
  public int CreatedBy { get; set; }
  public DateTime? UpdatedDate { get; set; }
  public int? UpdatedBy { get; set; }

  public CourseInformationDto? CourseInformation { get; set; }
  public EducationInformationDto? EducationInformation { get; set; }
  public AdditionalInformationDto? AdditionalInformation { get; set; }
}