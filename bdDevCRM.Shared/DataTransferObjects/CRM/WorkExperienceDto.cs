using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace bdDevCRM.Shared.DataTransferObjects.CRM;

public class WorkExperienceDto
{
  public List<WorkExperienceHistoryDto>? WorkExperienceHistory { get; set; }
  public int TotalWorkExperienceRecords { get; set; }
  public List<IFormFile>? ScannedCopyFileList { get; set; }
}