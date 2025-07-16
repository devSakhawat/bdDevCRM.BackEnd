using System;

namespace bdDevCRM.Shared.DataTransferObjects.CRM;

public class TOEFLInformationDto
{
  public int TOEFLInformationId { get; set; }

  public int ApplicantId { get; set; }

  public string? TOEFLListening { get; set; }

  public string? TOEFLReading { get; set; }

  public string? TOEFLWriting { get; set; }

  public string? TOEFLSpeaking { get; set; }

  public string? TOEFLOverallScore { get; set; }

  public DateTime? TOEFLDate { get; set; }

  public string? TOEFLScannedCopyPath { get; set; }

  public string? TOEFLAdditionalInformation { get; set; }

  public DateTime CreatedDate { get; set; }

  public int CreatedBy { get; set; }

  public DateTime? UpdatedDate { get; set; }

  public int? UpdatedBy { get; set; }
}