using System;

namespace bdDevCRM.Shared.DataTransferObjects.CRM;

public class OTHERSInformationDto
{
  public int OTHERSInformationId { get; set; }
  public int ApplicantId { get; set; } // Foreign Key
  public string? OTHERSAdditionalInformation { get; set; }
  public string? OTHERSScannedCopyPath { get; set; }
  
  // Common fields
  public DateTime CreatedDate { get; set; }
  public int CreatedBy { get; set; }
  public DateTime? UpdatedDate { get; set; }
  public int? UpdatedBy { get; set; }
}