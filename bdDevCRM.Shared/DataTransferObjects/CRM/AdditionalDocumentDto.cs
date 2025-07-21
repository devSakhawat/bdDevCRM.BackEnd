using Microsoft.AspNetCore.Http;
using System;

namespace bdDevCRM.Shared.DataTransferObjects.CRM;

public class AdditionalDocumentDto
{
  public int AdditionalDocumentId { get; set; }
  public int ApplicantId { get; set; } // Foreign Key
  public string? Title { get; set; }
  public string? UploadFile { get; set; }
  public string? DocumentName { get; set; }
  public string? FileThumbnail { get; set; }
  public string? RecordType { get; set; } = "Document"; // To distinguish from AdditionalInfo
  public IFormFile? UploadFileFormFile { get; set; }

  // Common fields
  public DateTime CreatedDate { get; set; }
  public int CreatedBy { get; set; }
  public DateTime? UpdatedDate { get; set; }
  public int? UpdatedBy { get; set; }
}