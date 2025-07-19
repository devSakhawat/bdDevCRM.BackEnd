using Microsoft.AspNetCore.Http;
using System;

namespace bdDevCRM.Shared.DataTransferObjects.CRM;

public class OTHERSInformationDto
{
  public int OTHERSInformationId { get; set; }

  public int ApplicantId { get; set; }

  public string? OTHERSAdditionalInformation { get; set; }

  public IFormFile? OTHERSScannedCopyFile { get; set; }
  public string? OTHERSScannedCopyFileName { get; set; }
  public string? OTHERSScannedCopyPath { get; set; }

  public DateTime CreatedDate { get; set; }

  public int CreatedBy { get; set; }

  public DateTime? UpdatedDate { get; set; }

  public int? UpdatedBy { get; set; }
}