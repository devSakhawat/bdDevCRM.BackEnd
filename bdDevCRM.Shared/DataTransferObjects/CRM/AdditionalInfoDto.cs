using Microsoft.AspNetCore.Http;
using System;

namespace bdDevCRM.Shared.DataTransferObjects.CRM;

public class AdditionalInfoDto
{
  public int AdditionalInfoId { get; set; }

  public int ApplicantId { get; set; }

  public string? RequireAccommodation { get; set; }

  public string? HealthNMedicalNeeds { get; set; }

  public string? HealthNMedicalNeedsRemarks { get; set; }

  public string? AdditionalInformationRemarks { get; set; }


  public string? DocumentTitle { get; set; }
  public string? UploadFile { get; set; }
  public string? DocumentName { get; set; }
  public string? FileThumbnail { get; set; }
  public IFormFile? UploadFileFormFile { get; set; }

  public string? RecordType { get; set; }

  public DateTime CreatedDate { get; set; }

  public int CreatedBy { get; set; }

  public DateTime? UpdatedDate { get; set; }

  public int? UpdatedBy { get; set; }
}