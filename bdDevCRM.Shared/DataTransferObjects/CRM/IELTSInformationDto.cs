using System;

namespace bdDevCRM.Shared.DataTransferObjects.CRM;

public class IELTSInformationDto
{
  public int IELTSInformationId { get; set; }
  public int ApplicantId { get; set; } // Foreign Key
  public string? IELTSListening { get; set; }
  public string? IELTSReading { get; set; }
  public string? IELTSWriting { get; set; }
  public string? IELTSSpeaking { get; set; }
  public string? IELTSOverallScore { get; set; }
  public DateTime? IELTSDate { get; set; }
  public string? IELTSScannedCopyPath { get; set; }
  public string? IELTSAdditionalInformation { get; set; }
  
  // Common fields
  public DateTime CreatedDate { get; set; }
  public int CreatedBy { get; set; }
  public DateTime? UpdatedDate { get; set; }
  public int? UpdatedBy { get; set; }
}