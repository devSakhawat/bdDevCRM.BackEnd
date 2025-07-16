using System;

namespace bdDevCRM.Shared.DataTransferObjects.CRM;

public class GMATInformationDto
{
  public int GMATInformationId { get; set; }

  public int ApplicantId { get; set; }

  public string? GMATListening { get; set; }

  public string? GMATReading { get; set; }

  public string? GMATWriting { get; set; }

  public string? GMATSpeaking { get; set; }

  public string? GMATOverallScore { get; set; }

  public DateTime? GMATDate { get; set; }

  public string? GMATScannedCopyPath { get; set; }

  public string? GMATAdditionalInformation { get; set; }

  public DateTime CreatedDate { get; set; }

  public int CreatedBy { get; set; }

  public DateTime? UpdatedDate { get; set; }

  public int? UpdatedBy { get; set; }
}