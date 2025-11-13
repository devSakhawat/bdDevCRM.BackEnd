using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdDevCRM.RepositoryDtos.CRM;

public class EducationHistoryRepositoryDto
{
  public int EducationHistoryId { get; set; }
  public int ApplicantId { get; set; }
  public string? Institution { get; set; }
  public string? Qualification { get; set; }
  public int? PassingYear { get; set; }
  public string? Grade { get; set; }

  public string? DocumentName { get; set; }
  public string? DocumentPath { get; set; }
  public string? AttachedDocument { get; set; }
  public string? PdfThumbnail { get; set; }

  public DateTime CreatedDate { get; set; }
  public int CreatedBy { get; set; }
  public DateTime? UpdatedDate { get; set; }
  public int? UpdatedBy { get; set; }
}

